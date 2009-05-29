using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace TibiaTekPlus.Plugins
{
    public partial class TestPluginMainForm : Form
    {
        TestPlugin plugin;

        public TestPluginMainForm()
        {
            InitializeComponent();
        }

        public TestPlugin Plugin
        {
            get
            {
                return plugin;
            }
            set
            {
                plugin = value;
            }
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            this.Hide();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            string output = "";
            foreach(IPlugin plug in Plugin.Host.Plugins){
                output += plug.Title + "\r\n";
            }
            textBox1.Text = output;
           // Plugin.Host.Client.Proxy.ReceivedTextMessageIncomingPacket += ReceivedTextMessageIncomingPacket;
        }

    }
}
