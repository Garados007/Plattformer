using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Plattformer.Control
{
    public static class MenuInputController
    {
        public static Dictionary<ActiveInputLayout, InputLayout> Layouts { get; private set; }

        public static ActiveInputLayout CurrentLayout { get; set; }

        static MenuInputController()
        {
            Layouts = new Dictionary<ActiveInputLayout, InputLayout>();
            foreach (ActiveInputLayout layout in Enum.GetValues(typeof(ActiveInputLayout)))
                Layouts.Add(layout, new InputLayout());
        }

        static Thread watchdog;
        static bool enablewatchdog;

        public static void StartWatchdog()
        {
            enablewatchdog = true;
            watchdog = new Thread(watchdogTask);
            watchdog.Name = "Input Controller Task";
            watchdog.Start();
        }

        static void watchdogTask()
        {
            while (enablewatchdog)
            {
                var layout = Layouts[CurrentLayout];
                layout.PerformInputAction();

                Thread.Sleep(40);
            }
        }

        public static void StopWatchdog()
        {
            enablewatchdog = false;
        }
    }

    public enum ActiveInputLayout
    {
        None,
        MainMenu,
    }

    public class InputLayout
    {
        public List<InputObjectEffect> Effects { get; private set; }

        public InputObjectEffect Current { get; private set; }

        public InputLayout(params InputObjectEffect[] effects)
        {
            Effects = effects.ToList();
        }

        InputType LastUsedInput = null;

        public void PerformInputAction()
        {
            if (Current == null)
            {
                if (Effects.Count == 0) return;
                Current = Effects[0];
                Current.InputObject.EnterFocus();
            }
            //determine possibles
            var possible = new List<InputType>();
            for (int i = 0; i < Current.Actions.Count; ++i)
                possible.Add(Current.Actions.ElementAt(i).Key);
            //determine actives
            var actives = new List<InputType>(possible.Count);
            foreach (var type in possible)
            {
                if (type.State == PressState.None) continue;
                if (InputManager.GetState(type.Axis) == type.State)
                    actives.Add(type);
            }
            if (actives.Count > 0)
            {
                if (actives.Count > 2)
                {
                    int time = InputManager.GetActionLastChanged(actives[0].Axis);
                    var result = actives[0];
                    for (int i = 1; i < actives.Count; ++i)
                    {
                        var t = InputManager.GetActionLastChanged(actives[i].Axis);
                        if (t < time)
                        {
                            time = t;
                            result = actives[i];
                        }
                    }
                    actives.Clear();
                    actives.Add(result);
                }
                //filter input
                var enableInput = true;
                if (actives[0] == LastUsedInput)
                {
                    var time = InputManager.GetActionLastChanged(actives[0].Axis);
                    var dif = Environment.TickCount - time;
                    if (dif < 700)
                        enableInput = false;
                }
                LastUsedInput = actives[0];
                //set next element
                if (enableInput)
                {
                    var obj = Current.Actions[actives[0]];
                    var eff = Effects.Find((e) => e.InputObject == obj);
                    if (eff != null)
                    {
                        Current.InputObject.LeaveFocus();
                        Current = eff;
                        Current.InputObject.EnterFocus();
                    }
                }
            }
            else
            {
                //check onclick
                var inpType = new InputType(InputAxis.Activate, PressState.PositivePressed);
                if (InputManager.GetPressed(InputAxis.Activate))
                {
                    if (LastUsedInput != inpType)
                    {
                        Current.InputObject.Click();
                        LastUsedInput = inpType;
                    }
                }
                else LastUsedInput = null;
            }
        }
    }

    public class InputObjectEffect
    {
        public IInputObject InputObject { get; private set; }

        public Dictionary<InputType, IInputObject> Actions { get; private set; }

        public InputObjectEffect(IInputObject inputObject)
        {
            this.InputObject = inputObject;
            this.Actions = new Dictionary<InputType, IInputObject>();
        }

        //chainable function
        public InputObjectEffect AddAction(InputType type, IInputObject inputObject)
        {
            Actions.Add(type, inputObject);
            return this;
        }

        //chainable function
        public InputObjectEffect AddAction(InputAxis axis, PressState state, IInputObject inputObject)
        {
            return AddAction(new InputType(axis, state), inputObject);
        }
    }

    public interface IInputObject
    {
        void EnterFocus();

        void LeaveFocus();

        void Click();
    }
}
