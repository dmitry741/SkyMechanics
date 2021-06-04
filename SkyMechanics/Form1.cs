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
        RectangleF _boundRect = RectangleF.Empty;

        #endregion

        #region private

        void CreateSolarSystem()
        {
            SkyBody skyBody;

            // звезда, центр планетарной системы
            skyBody = new SkyBody(Vector2.Zero, Vector2.Zero, 21, 8000, Color.Yellow);
            _skyBodies.Add(skyBody);

            // планета 1
            skyBody = new SkyBody(new Vector2(270, 0), new Vector2(0, -2.2f), 9, 40, Color.Wheat);
            _skyBodies.Add(skyBody);

            // спутник планеты 1
            skyBody = new SkyBody(new Vector2(295, 0), new Vector2(0, -3.05f), 5, 8, Color.LightGray);
            _skyBodies.Add(skyBody);

            // планета 2
            skyBody = new SkyBody(new Vector2(-360, 0), new Vector2(0, 1.7f), 13, 80, Color.Orange);
            _skyBodies.Add(skyBody);

            // спутник планеты 2
            skyBody = new SkyBody(new Vector2(-390, 0), new Vector2(0, 2.76f), 7, 8, Color.Tomato);
            _skyBodies.Add(skyBody);

            // астериод
            skyBody = new SkyBody(new Vector2(29, -150), new Vector2(-2, 0), 5, 2, Color.DodgerBlue);
            _skyBodies.Add(skyBody);
        }

        void RenderSkyBodies(Graphics g, SkyBodyManager skyBodies)
        {
            foreach(SkyBody skyBody in skyBodies)
            {
                RenderSkyBody(g, skyBody);
            }
        }

        void RenderTrace(Graphics g, SkyBody skyBody)
        {
            // TODO:
        }

        void RenderSkyBody(Graphics g, SkyBody skyBody)
        {
            Brush brush = new SolidBrush(skyBody.SBColor);
            RectangleF rf = new RectangleF(skyBody.Position.X - skyBody.R, skyBody.Position.Y - skyBody.R, skyBody.R * 2, skyBody.R * 2);
            g.FillEllipse(brush, rf);
        }

        void RenderBackSpace(Graphics g)
        {
            Bitmap backspace = Properties.Resources.back_space;
            RectangleF rf = new RectangleF(0, 0, pictureBox1.Width, pictureBox1.Height);
            g.DrawImage(backspace, rf);
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

        private void Form1_Load(object sender, EventArgs e)
        {
            CreateSolarSystem();
            _boundRect = _skyBodies.BoundRect;

            /*const float scaleFactor = 1.1f;
            float shiftX = _boundRect.Width * (scaleFactor - 1) / 2;
            float shiftY = _boundRect.Height * (scaleFactor - 1) / 2;

            _boundRect.Width *= scaleFactor;
            _boundRect.Height *= scaleFactor;
            _boundRect.X -= shiftX;
            _boundRect.Y -= shiftY;*/

            _bitmap = new Bitmap(pictureBox1.Width, pictureBox1.Height, System.Drawing.Imaging.PixelFormat.Format24bppRgb);            
            _timer.Tick += TimerTick;
        }

        private void TimerTick(object sender, EventArgs e)
        {
            _skyBodies.Next();
            Render();
        }

        #endregion

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
    }
}
