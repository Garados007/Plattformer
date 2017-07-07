using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Plattformer.UI
{
    public static class UIManager
    {
        private static Dictionary<RenderState, List<RenderLayer>> Layers = new Dictionary<RenderState, List<RenderLayer>>();

        public static RenderState CurrentRenderState = RenderState.LoadingScreen;

        static UIManager()
        {
            Layers.Add(RenderState.Menu, new List<RenderLayer>(new RenderLayer[]
            {
                new RenderMenuBackground(),
                new RenderMenu(Control.ActiveInputLayout.MainMenu),
#if DEBUG
                new Debug_ShowInputAxis(),
#endif
            }));
            Layers.Add(RenderState.Game, new List<RenderLayer>(new RenderLayer[]
            {
                new Level.LevelBackground(),
                new Level.VanillaLevelRenderer() { AccessKey = "LevelRenderer"},
#if DEBUG
                new Debug_ShowInputAxis(),
#endif
            }));
        }

        public static T GetLayer<T>(RenderState state, string key) where T : RenderLayer
        {
            if (!Layers.ContainsKey(state)) return null;
            var layer = Layers[state].Find((rl) => rl.AccessKey == key);
            return layer as T;
        }

        public static void LoadRessources()
        {
            for (int i = 0; i<Layers.Count; ++i)
            {
                var l = Layers.ElementAt(i).Value;
                for (int j = 0; j < l.Count; ++j)
                    l[j].LoadRessources();
            }
        }

        public static void Dispose()
        {
            for (int i = 0; i < Layers.Count; ++i)
            {
                var l = Layers.ElementAt(i).Value;
                for (int j = 0; j < l.Count; ++j)
                    l[j].Dispose();
            }
        }

        public static void Render(Graphics g, SizeF displaySize)
        {
            if (Layers.ContainsKey(CurrentRenderState))
            {
                var l = Layers[CurrentRenderState];
                for (int i = 0; i<l.Count; ++i)
                {
                    l[i].Render(g, displaySize);
                }
            }
        }
    }

    public enum RenderState
    {
        LoadingScreen,
        Menu,
        Game
    }

    public class RenderLayer : IDisposable
    {
        public string AccessKey { get; set; }

        public virtual void Dispose()
        {
        }

        public virtual void LoadRessources()
        {

        }

        public virtual void Render(Graphics g, SizeF displaySize)
        {

        }
    }
}
