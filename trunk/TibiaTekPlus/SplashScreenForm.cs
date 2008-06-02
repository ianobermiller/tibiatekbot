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

        private void close(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}
