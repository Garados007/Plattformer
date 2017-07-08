using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Plattformer.Control
{
    public static class LevelInputController
    {
        public static bool EnableInput { get; set; }

        public static Level.LevelContainer Level { get; set; }

        static Thread watchdog;
        static bool enablewatchdog;
        static Dictionary<InputAxis, PressState> lastPressState;

        static LevelInputController()
        {
            lastPressState = new Dictionary<InputAxis, PressState>();
            foreach (InputAxis axis in Enum.GetValues(typeof(InputAxis)))
                lastPressState.Add(axis, PressState.None);
        }

        public static void StartWatchdog()
        {
            enablewatchdog = true;
            watchdog = new Thread(watchdogTask);
            watchdog.Name = "Level Input Controller Task";
            watchdog.Start();
        }

        static void watchdogTask()
        {
            int lastTick = Environment.TickCount;
            while (enablewatchdog)
            {
                if (EnableInput)
                {
                    int tick = Environment.TickCount;
                    int tickDif = tick - lastTick;
                    lastTick = tick;
                    PressState state;
#if DEBUG
                    var camera = Level?.Camera;
                    if ((state = getPressFiltered(InputAxis.Debug_Zoom)) != PressState.None && camera != null)
                    {
                        try
                        {
                            if (state == PressState.PositivePressed)
                                camera.Distance *= 2;
                            else camera.Distance *= 0.5f;
                        }
                        catch (ArgumentOutOfRangeException) { }
                    }
                    camera.X += InputManager.GetAxisValue(InputAxis.horzMovement) * tickDif * 0.001f * 5;
                    camera.Y += InputManager.GetAxisValue(InputAxis.vertMovement) * tickDif * 0.001f * 5;
#endif
                }

                Thread.Sleep(40);
            }
        }

        public static void StopWatchdog()
        {
            enablewatchdog = false;
        }

        static PressState getPressFiltered(InputAxis axis)
        {
            var ps = InputManager.GetState(axis);
            var last = lastPressState[axis];
            lastPressState[axis] = ps;
            return ps == last ? PressState.None : ps;
        }
    }
}
