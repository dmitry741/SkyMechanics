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

        #endregion

        #region private

        void CreateSolarSystem()
        {
            // TODO:
        }

        void RenderSkyBodies(Graphics g, SkyBodyManager skyBodies)
        {
            // TODO:
        }

        void RenderTrace(Graphics g, SkyBody skyBody)
        {
            // TODO:
        }

        void RenderSkyBody(Graphics g, SkyBody skyBody)
        {

        }

        void Render()
        {
            if (_bitmap == null)
                return;

            Graphics g = Graphics.FromImage(_bitmap);
            g.Clear(Color.Black);

            // TODO:


            pictureBox1.Image = _bitmap;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
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
