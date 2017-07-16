using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using SharpDX.XInput;

namespace Plattformer.Control
{
    public static class GamePadController
    {
        static Thread watchdog;
        static bool enablewatchdog;

        public static void StartWatchdog()
        {
            enablewatchdog = true;
            watchdog = new Thread(watchdogTask);
            watchdog.Name = "XBox Input Controller Task";
            watchdog.Start();
        }

        static void watchdogTask()
        {
            var controller = new Controller(UserIndex.Any);
            while (enablewatchdog)
            {
                if (controller.IsConnected)
                {
                    var state = controller.GetState();
                    var pad = state.Gamepad;
                    InputManager.SetAction(StateSource.GamePad1, InputAxis.horzMovement,
                        getTriggerValue(pad.LeftThumbX, Gamepad.LeftThumbDeadZone));
                    InputManager.SetAction(StateSource.GamePad1, InputAxis.vertMovement,
                        getTriggerValue(pad.LeftThumbY, Gamepad.LeftThumbDeadZone));
                    InputManager.SetAction(StateSource.GamePad1, InputAxis.Jump,
                        pad.Buttons.HasFlag(GamepadButtonFlags.A));
                    InputManager.SetAction(StateSource.GamePad1, InputAxis.Pick,
                        pad.Buttons.HasFlag(GamepadButtonFlags.B));
                    InputManager.SetAction(StateSource.GamePad1, InputAxis.Activate,
                        pad.Buttons.HasFlag(GamepadButtonFlags.X));
#if DEBUG
                    InputManager.SetAction(StateSource.GamePad1, InputAxis.Debug_Zoom,
                        getTriggerValue(pad.RightThumbY, Gamepad.RightThumbDeadZone));
#endif
                }
                Thread.Sleep(40);
            }
        }

        static float getTriggerValue(short value, short dead)
        {
            const float minAxis = -32768;
            const float maxAxis =  32767;
            if (value >= -dead && value <= dead) return 0;
            if (value < 0) return -value / minAxis;
            return value / maxAxis;
        }

        public static void StopWatchdog()
        {
            enablewatchdog = false;
        }
    }
}
