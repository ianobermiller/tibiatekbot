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

        public int Percent {
            get
            {
                Application.DoEvents();
                return (int)Math.Floor((double)(pictureBox2.Size.Width * 100) / 308);
            }
            set
            {
                Size s = new Size((value * 308) / 100, 22);
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
