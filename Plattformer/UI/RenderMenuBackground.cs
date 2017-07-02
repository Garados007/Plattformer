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
            Background = Properties.Resources.bg_seemless;
            Player = new Bitmap[]
            {
                Properties.Resources.player_right,
                Properties.Resources.player_right2,
                Properties.Resources.player_right,
                Properties.Resources.player_right3,
            };
        }

        public override void Render(Graphics g, Size displaySize)
        {
            base.Render(g, displaySize);
            int time = Environment.TickCount;
            const int frameloop = 600;
            const float bgloop = 10 * frameloop;
            float bgOffset = -(time % bgloop) / bgloop;
            var bgw = (float)Background.Width * displaySize.Height / Background.Height;
            for (float x = bgOffset * bgw; x<displaySize.Width; x+=bgw)
                g.DrawImage(Background, x, 0, bgw, displaySize.Height);
            int frame = (time % frameloop) * Player.Length / frameloop;
            var tw = bgw * 0.1f;
            var th = Player[frame].Height * tw / Player[frame].Width;
            g.DrawImage(Player[frame], tw * 2, displaySize.Height - th, tw, th);
        }
    }
}
