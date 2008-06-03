using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using System.Reflection;
using System.Runtime.Remoting;
using System.Security;
using System.Security.Policy;
using System.Security.Permissions;
using TibiaTekPlus.Plugins;

namespace TibiaTekPlus
{
    public partial class PluginManagerForm : Form
    {
        private Kernel Kernel
        {
            get
            {
                return global::TibiaTekPlus.Program.kernel;
            }
        }


        public PluginManagerForm()
        {
            InitializeComponent();
        }

        private void installToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // AppDomain creation
            AppDomain domain = AppDomain.CreateDomain("PluginLoader");
            do {
                if (openPluginDialog.ShowDialog() == DialogResult.OK)
                {
                    string destDirectory = Application.StartupPath;
                    string destFileName = Path.Combine(destDirectory, openPluginDialog.SafeFileName);
                    File.Copy(openPluginDialog.FileName, destFileName, true);
                    string assemblyQualifiedName = Path.GetFileNameWithoutExtension(openPluginDialog.FileName);

                    /* Validate assembly as a valid plug-in */
                    bool valid = false;

                    Assembly assembly = domain.Load(assemblyQualifiedName);
                    Type pluginType = null;
                    foreach (Type type in assembly.GetTypes())
                    {
                        if (type.IsClass)
                        {
                            foreach (Type iface in type.GetInterfaces())
                            {
                                if (iface.Equals(typeof(IPlugin)))
                                {
                                    pluginType = type;
                                    valid = true;
                                    break;
                                }
                            }
                        }
                    }
                    
                    if (!valid)
                    {
                        if (MessageBox.Show("This is not a valid plug-in.", "Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error) == DialogResult.Retry)
                            continue;
                        else
                            break;
                    }

                    /*  */
                    valid = true;
                    IPlugin plugin = (IPlugin)domain.CreateInstanceAndUnwrap(assemblyQualifiedName, assemblyQualifiedName);
                    MessageBox.Show(plugin.Title);
                    //Activator.CreateInstance(domai
                    //IPlugin plugin = (IPlugin)Activator.CreateInstance(pluginType);
                    /*
                    foreach (IPlugin p in Kernel.plugins) {
                        if (p.Title.Equals(plugin.Title) && p.Version.Equals(plugin.Version))
                        {
                            valid = false;
                            break;
                        }
                    }
                    Kernel.plugins.Add(plugin);
            */
                    break;
                }
                else
                {
                    break;
                }
            } while (true);

            // Unload new AppDomain
            AppDomain.Unload(domain);
        }


    }
}
