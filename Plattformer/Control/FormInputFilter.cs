using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Plattformer.Control
{
    public static class FormInputFilter
    {
        static List<Tuple<InputAxis, PressState>> pressedButtons = new List<Tuple<InputAxis, PressState>>();

        public static void OnKeyDown(Keys key)
        {
            InputAxis axis;
            PressState direction;
            if (!GetAxis(key, out axis, out direction)) return;
            if (IsPressed(axis, direction)) return;
            pressedButtons.Add(new Tuple<InputAxis, PressState>(axis, direction));
            var opdir = direction == PressState.PositivePressed ? PressState.NegativePressed :
                direction == PressState.NegativePressed ? PressState.PositivePressed : PressState.None;
            var opposite = IsPressed(axis, opdir);
            InputManager.SetAction(axis, opposite ? PressState.None : direction);
        }

        public static void OnKeyUp(Keys key)
        {
            InputAxis axis;
            PressState direction;
            if (!GetAxis(key, out axis, out direction)) return;
            if (!IsPressed(axis, direction)) return;
            RemovePressed(axis, direction);
            var opdir = direction == PressState.PositivePressed ? PressState.NegativePressed :
                direction == PressState.NegativePressed ? PressState.PositivePressed : PressState.None;
            var opposite = IsPressed(axis, opdir);
            InputManager.SetAction(axis, opposite ? opdir : PressState.None);
        }

        static bool GetAxis(Keys key, out InputAxis axis, out PressState direction)
        {
            axis = InputAxis.Pick;
            direction = PressState.PositivePressed;
            switch (key)
            {
                case Keys.W:
                case Keys.Up:
                    axis = InputAxis.vertMovement;
                    direction = PressState.PositivePressed;
                    return true;
                case Keys.A:
                case Keys.Left:
                    axis = InputAxis.horzMovement;
                    direction = PressState.NegativePressed;
                    return true;
                case Keys.S:
                case Keys.Down:
                    axis = InputAxis.vertMovement;
                    direction = PressState.NegativePressed;
                    return true;
                case Keys.D:
                case Keys.Right:
                    axis = InputAxis.horzMovement;
                    direction = PressState.PositivePressed;
                    return true;
                case Keys.Space:
                    axis = InputAxis.Jump;
                    return true;
                case Keys.Q:
                case Keys.Enter:
                    axis = InputAxis.Activate;
                    return true;
                case Keys.E:
                case Keys.ControlKey:
                    axis = InputAxis.Pick;
                    return true;
                default: return false;
            }
        }

        static bool IsPressed(InputAxis axis, PressState direction)
        {
            for (int i = 0; i<pressedButtons.Count; ++i)
            {
                var t = pressedButtons[i];
                if (t.Item1 == axis && t.Item2 == direction) return true;
            }
            return false;
        }

        static void RemovePressed(InputAxis axis, PressState direction)
        {

            for (int i = 0; i < pressedButtons.Count; ++i)
            {
                var t = pressedButtons[i];
                if (t.Item1 == axis && t.Item2 == direction)
                {
                    pressedButtons.RemoveAt(i);
                    return;
                }
            }
        }
    }
}
