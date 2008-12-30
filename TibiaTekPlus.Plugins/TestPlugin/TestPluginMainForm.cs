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
            Plugin.Host.Client.Proxy.ReceivedTextMessageIncomingPacket += ReceivedTextMessageIncomingPacket;
        }

        bool ReceivedTextMessageIncomingPacket(Tibia.Packets.IncomingPacket p)
        {
            Tibia.Packets.Incoming.TextMessagePacket pp = (Tibia.Packets.Incoming.TextMessagePacket)p;
            setText(textBox1.Text + pp.Message + "\r\n");
            return true;
        }

        delegate void setTextDelegate(string value);

        void setText(string value)
        {
            
            if (textBox1.InvokeRequired)
            {
                textBox1.Invoke(new setTextDelegate(setText),value);
            }
            else
            {
                textBox1.Text = value;
            }
        }
    }
}
