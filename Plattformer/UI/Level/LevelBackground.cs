using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plattformer.UI.Level
{
    public class LevelBackground : RenderLayer
    {
        public override void Render(Graphics g, SizeF displaySize)
        {
            base.Render(g, displaySize);
            g.Clear(Color.White);
        }
    }
}
