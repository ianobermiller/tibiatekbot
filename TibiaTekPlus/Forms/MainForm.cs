using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Reflection;
using System.Collections;
using System.Collections.Specialized;
using System.Configuration;
using System.IO;
using TibiaTekPlus.Plugins;

namespace TibiaTekPlus
{
    public partial class MainForm : Form
    {

        private Kernel Kernel
        {
            get
            {
                return global::TibiaTekPlus.Program.kernel;
            }
        }

        public MainForm()
        {
            InitializeComponent();
        }

        private void testToolStripMenuItem_Click(object sender, EventArgs e)
        {
            /*
            AppDomain domain = AppDomain.CreateDomain("PluginLoader");
            Assembly assembly = domain.Load("TestPlugin");
            
            foreach (Type t in assembly.GetTypes())
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
            //Type tz = Type.GetType("TibiaTekPlus.Plugins.TestPlugin, TestPlugin");
            //object inst = Activator.CreateInstance(tz);
            //IPlugin plugin = (IPlugin)inst;
            //this.PluginIcon = plugin.PluginIcon;
            //plugin.Show();
            
            //ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None).Save(ConfigurationSaveMode.Minimal, true);
            //ConfigurationManager.RefreshSection("appSettings");
        }

        private void b_Click(object sender, System.Windows.RoutedEventArgs e){
            
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void pluginManagerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Kernel.pluginsForm.Show();
            //this.en
        }

        private void listView1_ItemActivate(object sender, EventArgs e)
        {
            //if (listView1.FocusedItem == null)
            //    return;
            //ListViewItem item = listView1.FocusedItem;
            //if (item == null)
            //    return;
            //if (item.Tag == null)
            //    return;
            //switch (item.Tag.ToString())
            //{
            //    case "pluginManager":
            //        Kernel.pluginsForm.Show();
            //        break;
            //    case "customize":
                    
            //        break;
            //}
        }



    }
}
