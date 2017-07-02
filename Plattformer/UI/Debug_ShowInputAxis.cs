using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plattformer.UI
{
#if DEBUG
    public class Debug_ShowInputAxis : RenderLayer
    {
        Font font;
        Control.ActionEvent[] actions;

        public override void LoadRessources()
        {
            base.LoadRessources();
            font = new Font(FontFamily.GenericSansSerif, 10);
            actions = (Control.ActionEvent[])Enum.GetValues(typeof(Control.ActionEvent));
        }

        public override void Dispose()
        {
            base.Dispose();
            font.Dispose();
        }

        public override void Render(Graphics g, SizeF displaySize)
        {
            base.Render(g, displaySize);
            var t = "";
            foreach (var action in actions)
            {
                t += action.ToString() + ": " + Control.InputManager.GetAxisValue(action);
                t += Environment.NewLine;
            }
            var size = g.MeasureString(t, font);
            g.DrawString(t, font, Brushes.Black, displaySize.Width - size.Width, displaySize.Height - size.Height);
        }
    }
#endif
}
