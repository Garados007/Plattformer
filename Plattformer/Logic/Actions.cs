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
                OpenMenu(false);
            };
            if (async) new Task(task).Start();
            else task();
        }
    }
}
