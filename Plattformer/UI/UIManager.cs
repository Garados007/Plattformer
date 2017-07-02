﻿using System;
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

        public static RenderState CurrentRenderState = RenderState.Menu;

        static UIManager()
        {
            Layers.Add(RenderState.Menu, new List<RenderLayer>(new RenderLayer[]
            {

            }));
        }

        public static void Render(Graphics g, Size displaySize)
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

    public class RenderLayer
    {
        public virtual void Render(Graphics g, Size displaySize)
        {

        }
    }
}
