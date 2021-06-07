using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Numerics;

namespace SkyMechanics
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        #region memebers

        Bitmap _bitmap = null;
        readonly Timer _timer = new Timer { Enabled = false };
        readonly SkyBodyManager _skyBodies = new SkyBodyManager();
        RectangleF _winBoundRect = RectangleF.Empty;
        RectangleF _realBoundRect = RectangleF.Empty;

        #endregion

        #region private

        void CreateSolarSystem()
        {
            SkyBody skyBody;

            // звезда, центр планетарной системы
            skyBody = new SkyBody(Vector2.Zero, Vector2.Zero, 21, 3000, Color.Yellow);
            _skyBodies.Add(skyBody);

            // планета 1
            skyBody = new SkyBody(new Vector2(270, 0), new Vector2(0, -2.2f), 9, 40, Color.Wheat);
            _skyBodies.Add(skyBody);

            // спутник планеты 1
            skyBody = new SkyBody(new Vector2(295, 0), new Vector2(0, -3.05f), 4, 8, Color.LightGray);
            _skyBodies.Add(skyBody);

            // планета 2
            skyBody = new SkyBody(new Vector2(-360, 0), new Vector2(0, 1.7f), 13, 80, Color.Orange);
            _skyBodies.Add(skyBody);

            // спутник планеты 2
            skyBody = new SkyBody(new Vector2(-390, 0), new Vector2(0, 2.76f), 6, 8, Color.Tomato);
            _skyBodies.Add(skyBody);

            // планета 3
            skyBody = new SkyBody(new Vector2(0, 310), new Vector2(2.1f, 0), 8, 10, Color.Red);
            _skyBodies.Add(skyBody);

            // астериод
            skyBody = new SkyBody(new Vector2(29, -150), new Vector2(-2, 0), 5, 2, Color.White);
            _skyBodies.Add(skyBody);
        }

        void RenderSkyBodies(Graphics g, SkyBodyManager skyBodies)
        {
            foreach(SkyBody skyBody in skyBodies)
            {
                RenderSkyBody(g, skyBody);
            }
        }

        void RenderSkyBody(Graphics g, SkyBody skyBody)
        {
            Brush brush = new SolidBrush(skyBody.SBColor);
            PointF point = ConvertToWin(skyBody.Position.X, skyBody.Position.Y, _winBoundRect, _realBoundRect);
            RectangleF rf = new RectangleF(point.X - skyBody.R, point.Y - skyBody.R, skyBody.R * 2, skyBody.R * 2);
            g.FillEllipse(brush, rf);
        }

        void RenderBackSpace(Graphics g)
        {
            Bitmap backspace = Properties.Resources.back_space;
            g.DrawImage(backspace, 0, 0);
        }

        void Render()
        {
            if (_bitmap == null)
                return;

            Graphics g = Graphics.FromImage(_bitmap);

            RenderBackSpace(g);
            RenderSkyBodies(g, _skyBodies);

            pictureBox1.Image = _bitmap;
        }

        PointF ConvertToWin(float X, float Y, RectangleF winBoundRect, RectangleF realBoundRect)
        {
            float Xwin = winBoundRect.Width / realBoundRect.Width * (X - realBoundRect.X) + winBoundRect.X;
            float Ywin = winBoundRect.Bottom - winBoundRect.Height / realBoundRect.Height * (realBoundRect.Bottom - Y);
            return new PointF(Xwin, Ywin);
        }

        RectangleF WinBoundRect
        {
            get
            {
                RectangleF rpb = new RectangleF(0, 0, pictureBox1.Width, pictureBox1.Height);
                rpb.Inflate(-8, -8);

                RectangleF realBoundRect = _skyBodies.BoundRect;
                float scaleFactor = rpb.Width / realBoundRect.Width;
                RectangleF resultRect = RectangleF.Empty;

                if (realBoundRect.Height * scaleFactor < rpb.Height)
                {
                    resultRect.X = rpb.X;
                    resultRect.Y = rpb.Y + rpb.Height / 2 - realBoundRect.Height * scaleFactor / 2;
                    resultRect.Width = rpb.Width;
                    resultRect.Height = realBoundRect.Height * scaleFactor;
                }
                else
                {
                    scaleFactor = rpb.Height / realBoundRect.Height;
                    resultRect.X = rpb.X + rpb.Width / 2 - realBoundRect.Width * scaleFactor / 2;
                    resultRect.Y = rpb.Y;
                    resultRect.Width = realBoundRect.Width * scaleFactor;
                    resultRect.Height = rpb.Height;
                }

                return resultRect;
            }
        }

        #endregion

        private void Form1_Load(object sender, EventArgs e)
        {
            CreateSolarSystem();
            _winBoundRect = WinBoundRect;
            _realBoundRect = _skyBodies.BoundRect;

            _bitmap = new Bitmap(pictureBox1.Width, pictureBox1.Height, System.Drawing.Imaging.PixelFormat.Format24bppRgb);            
            _timer.Tick += TimerTick;
        }

        private void TimerTick(object sender, EventArgs e)
        {
            _skyBodies.Next();
            Render();
        }       

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Render();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            _skyBodies.Next();
            Render();
        }

        private void btnStart_Click(object sender, EventArgs e)
        {            
            if (!_timer.Enabled)
            {
                _timer.Interval = 50;
                _timer.Start();
            }
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            if (_timer.Enabled)
            {
                _timer.Stop();
            }
        }

        private void btnZoomToFit_Click(object sender, EventArgs e)
        {
            _winBoundRect = WinBoundRect;
            _realBoundRect = _skyBodies.BoundRect;

            if (!_timer.Enabled)
            {
                Render();
            }
        }
    }
}
