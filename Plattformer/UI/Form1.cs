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
                Buffer.Render();
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
    }
}
