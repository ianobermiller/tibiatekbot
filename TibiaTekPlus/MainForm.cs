using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Reflection;
using TibiaTekBot.Plugins;

namespace TibiaTekBot
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void testToolStripMenuItem_Click(object sender, EventArgs e)
        {
            /*
            AppDomain domain = AppDomain.CreateDomain("PluginLoader");
            Assembly asm = domain.Load("TestPlugin");
            
            foreach (Type t in asm.GetTypes())
            {
                if (t.IsClass)
                {
                    foreach (Type iface in t.GetInterfaces())
                    {
                        if (iface.Equals(typeof(IPlugin)))
                        {
                            break;
                        }
                    }
                }
            }
            AppDomain.Unload(domain);
            */
            //Application.UserAppDataPath
            //Type tz = Type.GetType("TibiaTekBot.Plugins.TestPlugin, TestPlugin");
            //object inst = Activator.CreateInstance(tz);
            //IPlugin plugin = (IPlugin)inst;
            //this.Icon = plugin.Icon;
            //plugin.Show();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
