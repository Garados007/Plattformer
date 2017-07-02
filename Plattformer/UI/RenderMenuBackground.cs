using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plattformer.UI
{
    public class RenderMenuBackground : RenderLayer
    {
        Bitmap Background;
        Bitmap[] Player;

        public override void LoadRessources()
        {
            base.LoadRessources();
            Background = Properties.Resources.bg;
            Player = new Bitmap[]
            {
                Properties.Resources.player_right,
                Properties.Resources.player_right2,
                Properties.Resources.player_right3,
            };
        }

        public override void Render(Graphics g, Size displaySize)
        {
            base.Render(g, displaySize);
            g.DrawImage(Background, new Rectangle(new Point(), displaySize));

        }
    }
}
