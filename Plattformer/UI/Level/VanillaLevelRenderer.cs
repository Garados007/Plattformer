using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Plattformer.Level;

namespace Plattformer.UI.Level
{
    public class VanillaLevelRenderer : LevelRendererBase
    {
#if DEBUG
        Pen debug_framepen;
#endif

        public override void LoadRessources()
        {
            base.LoadRessources();
#if DEBUG
            debug_framepen = new Pen(Color.FromArgb(0x80, Color.Black), 1);
#endif
        }

        public override void Dispose()
        {
            base.Dispose();
#if DEBUG
            debug_framepen.Dispose();
#endif
        }

        protected override void renderLevelCell(Graphics g, LevelCell cell, RectangleF bounds, int time)
        {


#if DEBUG
            g.DrawRectangle(debug_framepen, Rectangle.Round(bounds));
#endif
        }

        protected override void renderSprite(Graphics g, Sprite sprite, RectangleF bounds, int time)
        {
        }
    }
}
