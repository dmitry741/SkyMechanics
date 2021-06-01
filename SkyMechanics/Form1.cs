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
        Timer _timer = null;

        #endregion

        #region private

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
            // TODO:    
        }

        #endregion

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Render();
        }
    }
}
