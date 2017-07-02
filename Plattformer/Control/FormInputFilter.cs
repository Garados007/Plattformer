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
        static List<Tuple<ActionEvent, PressState>> pressedButtons = new List<Tuple<ActionEvent, PressState>>();

        public static void OnKeyDown(Keys key)
        {
            ActionEvent axis;
            PressState direction;
            if (!GetAxis(key, out axis, out direction)) return;
            if (IsPressed(axis, direction)) return;
            pressedButtons.Add(new Tuple<ActionEvent, PressState>(axis, direction));
            var opdir = direction == PressState.PositivePressed ? PressState.NegativePressed :
                direction == PressState.NegativePressed ? PressState.PositivePressed : PressState.None;
            var opposite = IsPressed(axis, opdir);
            InputManager.SetAction(axis, opposite ? PressState.None : direction);
        }

        public static void OnKeyUp(Keys key)
        {
            ActionEvent axis;
            PressState direction;
            if (!GetAxis(key, out axis, out direction)) return;
            if (!IsPressed(axis, direction)) return;
            RemovePressed(axis, direction);
            var opdir = direction == PressState.PositivePressed ? PressState.NegativePressed :
                direction == PressState.NegativePressed ? PressState.PositivePressed : PressState.None;
            var opposite = IsPressed(axis, opdir);
            InputManager.SetAction(axis, opposite ? opdir : PressState.None);
        }

        static bool GetAxis(Keys key, out ActionEvent axis, out PressState direction)
        {
            axis = ActionEvent.Pick;
            direction = PressState.PositivePressed;
            switch (key)
            {
                case Keys.W:
                case Keys.Up:
                    axis = ActionEvent.vertMovement;
                    direction = PressState.PositivePressed;
                    return true;
                case Keys.A:
                case Keys.Left:
                    axis = ActionEvent.horzMovement;
                    direction = PressState.NegativePressed;
                    return true;
                case Keys.S:
                case Keys.Down:
                    axis = ActionEvent.vertMovement;
                    direction = PressState.NegativePressed;
                    return true;
                case Keys.D:
                case Keys.Right:
                    axis = ActionEvent.horzMovement;
                    direction = PressState.PositivePressed;
                    return true;
                case Keys.Space:
                    axis = ActionEvent.Jump;
                    return true;
                case Keys.Q:
                case Keys.Enter:
                    axis = ActionEvent.Pick;
                    return true;
                case Keys.E:
                case Keys.ControlKey:
                    axis = ActionEvent.Activate;
                    return true;
                default: return false;
            }
        }

        static bool IsPressed(ActionEvent axis, PressState direction)
        {
            for (int i = 0; i<pressedButtons.Count; ++i)
            {
                var t = pressedButtons[i];
                if (t.Item1 == axis && t.Item2 == direction) return true;
            }
            return false;
        }

        static void RemovePressed(ActionEvent axis, PressState direction)
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
