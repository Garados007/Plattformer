using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace Plattformer.UI
{
    public partial class Form1 : Form
    {
        Thread renderThread;
        BufferedGraphics Buffer;
        Size LastViewSize;
        Graphics FormGraphics;
        public bool IsFullScreen { get; private set; }
        Rectangle windowBounds;
        FormBorderStyle windowBorder;


        public Form1()
        {
            IsFullScreen = false;
            InitializeComponent();
            this.Load += Form1_Load;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            FormGraphics = CreateGraphics();
            renderThread = new Thread(RenderLoop);
            renderThread.Name = "Render Thread of UI";
            renderThread.Start();
            Logic.Actions.Startup();
        }

        void SetupBuffer()
        {
            if (LastViewSize != ClientSize)
            {
                LastViewSize = ClientSize;
                if (Buffer != null) Buffer.Dispose();
                Buffer = BufferedGraphicsManager.Current.Allocate(FormGraphics, new Rectangle(new Point(), LastViewSize));
            }
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
        }

        void RenderLoop()
        {
            while (!Disposing && !IsDisposed)
            {
                var start = Environment.TickCount;
                SetupBuffer();
                RenderFunction();
#if DEBUG
                if (MonitorFPS)
                    DrawMonitorFPS(Buffer.Graphics, Environment.TickCount - start, 0, 60, 1, LastViewSize.Width);
#endif
                try { Buffer.Render(); }
                catch
                {
                    if (Disposing || IsDisposed) break;
                    throw;
                }
                var finished = Environment.TickCount;
                var time = 40 - ((finished - start) % 40);
                Thread.Sleep(time);
            }
        }

        void RenderFunction()
        {
            UIManager.Render(Buffer.Graphics, LastViewSize);
        }

        public void ToggleFullscreen()
        {
            if (IsFullScreen = !IsFullScreen)
            {
                windowBounds = Bounds;
                windowBorder = this.FormBorderStyle;
                this.FormBorderStyle = FormBorderStyle.None;
                this.Bounds = Screen.FromRectangle(Bounds).Bounds;
            }
            else
            {
                Bounds = windowBounds;
                FormBorderStyle = windowBorder;
            }
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);
            if (e.KeyCode == Keys.F11)
            {
                ToggleFullscreen();
            }
        }

#if DEBUG
        public static bool MonitorFPS = true;

        float[] timebuffer = new float[50];
        int timepos = 0;
        float lastmaxb = 0;

        private void DrawMonitorFPS(Graphics g, float fill, float min, float max, float step, int width)
        {
            timebuffer[timepos] = fill;
            timepos = (timepos + 1) % timebuffer.Length;
            var maxb = timebuffer[0];
            for (int i = 1; i < timebuffer.Length; ++i)
                maxb = Math.Max(maxb, timebuffer[i]);
            if (maxb > lastmaxb) lastmaxb = maxb;
            else lastmaxb = maxb =
                maxb + (lastmaxb - maxb) * 0.7f;

            width = Math.Min(width, 400);

            var r = new Rectangle(10, 10, width - 20, 10);
            var f = new RectangleF(10, 10,
                r.Width * (fill - min) / (max - min), 10);
            g.FillRectangle(maxb < max ? Brushes.LightGreen : Brushes.Red, f);
            g.DrawRectangle(Pens.Green, r);
            for (float v = min, i = 0; v <= max; v += step, i++)
            {
                var l = 10 + r.Width * (v - min) / (max - min);
                g.DrawLine(Pens.Green, l, 20, l,
                    25 + (i % 5 == 0 ? 5 : 0) + (i % 20 == 0 ? 5 : 0));
            }
            var maxbl = 10 + r.Width * (maxb - min) / (max - min);
            g.DrawLine(Pens.Green, maxbl, 12, maxbl, 18);
        }
#endif
    }
}
