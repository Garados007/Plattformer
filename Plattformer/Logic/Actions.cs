using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plattformer.Logic
{
    public static class Actions
    {
        public static void OpenMenu(bool async = true)
        {
            Action task = () =>
            {
                UI.UIManager.CurrentRenderState = UI.RenderState.Menu;
            };
            if (async) new Task(task).Start();
            else task();
        }

        public static void LoadRessources(bool async = true)
        {
            Action task = () =>
            {
                UI.UIManager.LoadRessources();
            };
            if (async) new Task(task).Start();
            else task();
        }

        public static void Startup(bool async = true)
        {
            Action task = () =>
            {
                LoadRessources(false);
                Control.InputManager.StartWatchdog();
                Control.MenuInputController.StartWatchdog();
                SetInputLayout(Control.ActiveInputLayout.MainMenu, false);
                OpenMenu(false);
            };
            if (async) new Task(task).Start();
            else task();
        }
        
        public static void Cleanup(bool async = true)
        {
            Action task = () =>
            {
                Control.InputManager.StopWatchdog();
                Control.MenuInputController.StopWatchdog();
                UI.UIManager.Dispose();
            };
            if (async) new Task(task).Start();
            else task();
        }

        public static void CloseApp(bool async = true)
        {
            Action task = () =>
            {
                var form = System.Windows.Forms.Form.ActiveForm;
                form.Invoke(new Action(form.Close));
            };
            if (async) new Task(task).Start();
            else task();
        }

        public static void SetInputLayout(Control.ActiveInputLayout layout, bool async = true)
        {
            Action task = () =>
            {
                Control.MenuInputController.CurrentLayout = layout;
            };
            if (async) new Task(task).Start();
            else task();
        }
    }
}
