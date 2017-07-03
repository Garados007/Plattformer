using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Plattformer.Control;

namespace Plattformer.UI
{
    public class RenderMenu : RenderLayer
    {
        RenderMenuButton[] buttons;
        public RenderMenuButton[] Buttons => buttons;

        public ActiveInputLayout InputLayout { get; private set; }

        public RenderMenu(ActiveInputLayout inputLayout)
        {
            this.InputLayout = inputLayout;
            switch (inputLayout)
            {
                case ActiveInputLayout.MainMenu:
                    buttons = new RenderMenuButton[]
                    {
                        new RenderMenuButton("Spiel starten"),
                        new RenderMenuButton("Optionen"),
                        new RenderMenuButton("Spiel beenden", () => Logic.Actions.CloseApp()),
                    };
                    break;
            }
            var effects = MenuInputController.Layouts[inputLayout].Effects;
            for (int i = 0; i < buttons.Length; ++i)
            {
                effects.Add(
                    new InputObjectEffect(buttons[i])
                    .AddAction(InputAxis.vertMovement, PressState.PositivePressed, buttons[(i + buttons.Length - 1) % buttons.Length])
                    .AddAction(InputAxis.vertMovement, PressState.NegativePressed, buttons[(i + 1) % buttons.Length]));
            }
        }

        public override void LoadRessources()
        {
            base.LoadRessources();
            foreach (var button in buttons)
                button.LoadRessources();
        }

        public override void Dispose()
        {
            base.Dispose();
            foreach (var button in buttons)
                button.Dispose();
        }

        public override void Render(Graphics g, SizeF displaySize)
        {
            base.Render(g, displaySize);
            var bw = displaySize.Width / 2;
            var bh = displaySize.Height / (buttons.Length * 2 + 3);
            var size = new SizeF(bw, bh);
            var t = g.Transform;
            g.TranslateTransform(bw / 2, 2 * bh);
            for (int i = 0; i<buttons.Length; ++i)
            {
                buttons[i].Render(g, size);
                g.TranslateTransform(0, 2 * bh);
            }
            g.Transform = t;
        }
    }
    
    public class RenderMenuButton : RenderLayer, Control.IInputObject
    {
        public string Text { get; private set; }

        public event Action OnClick;

        public RenderMenuButton() { }
        public RenderMenuButton(string text)
        {
            Text = text;
        }
        public RenderMenuButton(string text, Action onClick)
        {
            Text = text;
            OnClick += onClick;
        }

        Font font;
        Brush normalBackground, activeBackground;
        bool active = false;

        public override void LoadRessources()
        {
            base.LoadRessources();
            font = new Font(FontFamily.GenericSansSerif, 12);
            normalBackground = new SolidBrush(Color.FromArgb(0x80, Color.Black));
            activeBackground = new SolidBrush(Color.FromArgb(0x80, Color.Yellow));
        }

        public override void Dispose()
        {
            base.Dispose();
            font.Dispose();
            normalBackground.Dispose();
        }

        public override void Render(Graphics g, SizeF displaySize)
        {
            base.Render(g, displaySize);
            g.FillRectangle(active ? activeBackground : normalBackground, 0, 0, displaySize.Width, displaySize.Height);
            if (Text == null) return;
            var fsize = g.MeasureString(Text, font);
            g.DrawString(Text, font, Brushes.Black, (displaySize.Width - fsize.Width) / 2, (displaySize.Height - fsize.Height) / 2);
        }

        public void EnterFocus()
        {
            active = true;
        }

        public void LeaveFocus()
        {
            active = false;
        }

        public void Click()
        {
            OnClick?.Invoke();
        }
    }
}
