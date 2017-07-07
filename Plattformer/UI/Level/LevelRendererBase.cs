using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Plattformer.Level;
using System.Drawing;

namespace Plattformer.UI.Level
{
    public abstract class LevelRendererBase : RenderLayer
    {
        public LevelContainer Level { get; set; }

        protected virtual void renderLevelCell(Graphics g, LevelCell cell, RectangleF bounds, int time)
        {

        }

        protected virtual void renderSprite(Graphics g, Sprite sprite, RectangleF bounds, int time)
        {

        }
                
        public override void Render(Graphics g, SizeF displaySize)
        {
            base.Render(g, displaySize);
            var level = Level;
            if (level == null) return;
            int time = Environment.TickCount;
            float l, r, t, b;
            level.Camera.MeasureDisplay(displaySize.Width / displaySize.Height, out l, out t, out r, out b);
            var tileSize = new SizeF(displaySize.Width / (r - l), displaySize.Height / (t - b));
            var screenMiddle = new PointF(displaySize.Width / 2, displaySize.Height / 2);
            int ol = Math.Max(0, (int)Math.Floor(l)), or = Math.Min(level.Width - 1, (int)Math.Ceiling(r));
            int ob = Math.Max(0, (int)Math.Floor(b)), ot = Math.Min(level.Height - 1, (int)Math.Ceiling(r));
            float hm = (r + l) * .5f, vm = (t + b) * .5f;
            for (int x = ol; x<=or; ++x)
                for (int y = ob; y<=ot; ++y)
                {
                    var bounds = new RectangleF(
                        screenMiddle.X + tileSize.Width * (x - hm),
                        screenMiddle.Y - tileSize.Height * (y - vm),
                        tileSize.Width,
                        tileSize.Height);
                    renderLevelCell(g, level.Cells[x, y], bounds, time);
                }
            foreach (var sprite in level.Sprites.ToArray())
                if ((IsValueBetween(sprite.X, l, r) || IsValueBetween(sprite.X + sprite.Width, l, r)) && 
                    (IsValueBetween(sprite.Y, b, t) || IsValueBetween(sprite.Y + sprite.Height, b, t)))
                {
                    var bounds = new RectangleF(
                        screenMiddle.X + tileSize.Width * (sprite.X - hm),
                        screenMiddle.Y - tileSize.Height * (sprite.Y - vm),
                        tileSize.Width * sprite.Width,
                        tileSize.Height * sprite.Height);
                    renderSprite(g, sprite, bounds, time);
                }
        }

        private bool IsValueBetween(float value, float lowerBound, float higherBound)
        {
            return value >= lowerBound && value <= higherBound;
        }
    }
}
