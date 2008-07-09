using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace TibiaTekPlus
{
    public partial class SplashScreenForm : Form
    {
        public SplashScreenForm()
        {
            InitializeComponent();
            this.DialogResult = DialogResult.OK;
        }

        public void SetText(string text){
            pictureBox2.Refresh();
            Graphics g = pictureBox2.CreateGraphics();

        }

        public double Percent {
            get
            {
                Application.DoEvents();
                return ((pictureBox2.Size.Width * 100.0) / 306.0);
            }
            set
            {
                Size s = (new SizeF((float)((value * 306.0) / 100.0), (float)20.0)).ToSize();
                pictureBox2.Size = s;
                Application.DoEvents();
            }
        }

        private void SplashScreenForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            System.Threading.Thread.Sleep(1500);
        }

    }
}
