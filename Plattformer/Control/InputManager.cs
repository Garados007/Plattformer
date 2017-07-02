using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Plattformer.Control
{
    public static class InputManager
    {
        static Dictionary<ActionEvent, float> actionAxis = new Dictionary<ActionEvent, float>();
        static Dictionary<ActionEvent, PressState> lastAxisState = new Dictionary<ActionEvent, PressState>();
        static Dictionary<ActionEvent, int> axisLastChange = new Dictionary<ActionEvent, int>();
        static Thread Watchdog;
        static bool watchdogEnabled;

        static InputManager()
        {
            var time = Environment.TickCount;
            foreach (ActionEvent action in Enum.GetValues(typeof(ActionEvent)))
            {
                actionAxis.Add(action, 0);
                lastAxisState.Add(action, PressState.None);
                axisLastChange.Add(action, time);
            }
        }

        public static float Epsilon = 0.1f;

        public static bool GetPressed(ActionEvent action)
        {
            return Math.Abs(actionAxis[action]) >= Epsilon;
        }

        public static PressState GetState(ActionEvent action)
        {
            var value = actionAxis[action];
            if (value <= -Epsilon) return PressState.NegativePressed;
            if (value >= Epsilon) return PressState.PositivePressed;
            return PressState.None;
        }

        public static float GetAxisValue(ActionEvent action)
        {
            return actionAxis[action];
        }

        public static void SetAction(ActionEvent action, bool pressed)
        {
            actionAxis[action] = pressed ? 1 : 0;
        }

        public static void SetAction(ActionEvent action, PressState state)
        {
            actionAxis[action] = state == PressState.NegativePressed ? -1 : state == PressState.PositivePressed ? 1 : 0;
        }

        public static void SetAction(ActionEvent action, float value)
        {
            actionAxis[action] = Math.Max(-1, Math.Min(1, value));
         }

        public static int GetActionLastChanged(ActionEvent action)
        {
            return axisLastChange[action];
        }

        public static void StartWatchdog()
        {
            watchdogEnabled = true;
            Watchdog = new Thread(watchdogTask);
            Watchdog.Name = "Input Manager Watchdog";
            Watchdog.Start();
        }

        //watchdog detect long pressed buttons
        static void watchdogTask()
        {
            while (watchdogEnabled)
            {
                var time = Environment.TickCount;
                for (var i = 0; i<actionAxis.Count; ++i)
                {
                    var key = actionAxis.ElementAt(i).Key;
                    var cur = GetState(key);
                    var last = lastAxisState.ElementAt(i).Value;
                    if (cur != last)
                        axisLastChange[key] = time;
                }
                Thread.Sleep(100);
            }
        }

        public static void StopWatchdog()
        {
            watchdogEnabled = false;
        }
    }

    public enum ActionEvent
    {
        /// <summary>
        /// [-] down, [+] up
        /// </summary>
        vertMovement,
        /// <summary>
        /// [-] left, [+] right
        /// </summary>
        horzMovement,
        Jump,
        Pick,
        Activate,
    }

    public enum PressState
    {
        None,
        PositivePressed,
        NegativePressed,
    }
}
