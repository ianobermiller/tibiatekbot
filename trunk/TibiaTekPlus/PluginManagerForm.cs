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
using System.Xml;

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
                    string fname = Path.GetFileNameWithoutExtension(openPluginDialog.FileName);

                    /* Validate assembly as a valid plug-in */
                    bool valid = false;

                    Assembly assembly = domain.Load(fname);
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
                    Plugin plugin = (Plugin)domain.CreateInstanceAndUnwrap(fname, fname);
                    //MessageBox.Show(plugin.Title);
                    
                    // Obtain the real fully assembly qualified name
                    fname = plugin.GetType().AssemblyQualifiedName;

                    XmlDocument document = new XmlDocument();
                    document.Load("TibiaTekPlus.Plugins.xml");

                    XmlElement plugins = (XmlElement)document["plugins"];

                    // Check if the plug-in is scheduled to be installed
                    foreach (XmlElement installPlugin in plugins["pending"]["install"])
                    {
                        if (fname.Equals(installPlugin.GetAttribute("fullname")) && plugin.Version.Equals(installPlugin["version"])) {
                            MessageBox.Show("This plug-in is already scheduled to be installed. Please close all instances of TibiaTek Plus first.");
                            break;
                        }
                    }

                    // Check if the plug-in is scheduled to be uninstalled
                    foreach (XmlElement uninstallPlugin in plugins["pending"]["uninstall"])
                    {
                        if (fname.Equals(uninstallPlugin.GetAttribute("fullname")) && plugin.Version.Equals(uninstallPlugin["version"].InnerText))
                        {
                            MessageBox.Show("This plug-in is scheduled to be uninstalled. Please close all instances of TibiaTek Plus first.");
                            break;
                        }
                    }

                    // Check if the plug-in is already installed
                    foreach (XmlElement installedPlugin in plugins["pending"]["uninstall"])
                    {
                        if (fname.Equals(installedPlugin.GetAttribute("fullname")) && plugin.Version.Equals(installedPlugin["version"].InnerText))
                        {
                            MessageBox.Show("This plug-in is already installed.");
                            break;
                        }
                    }
                   
                    XmlElement xplugin = document.CreateElement("plugin");

                    // Full Name
                    XmlAttribute fullname = document.CreateAttribute("fullname");
                    fullname.Value = plugin.GetType().FullName;
                    xplugin.Attributes.Append(fullname);

                    // Version
                    XmlAttribute version = document.CreateAttribute("version");
                    version.Value = plugin.Version;
                    xplugin.Attributes.Append(version);

                    // Title
                    XmlElement title = document.CreateElement("title");
                    title.InnerText = plugin.Title;
                    xplugin.AppendChild(title);

                    // Author
                    XmlElement author = document.CreateElement("author");
                    author.InnerText = plugin.Author;
                    xplugin.AppendChild(author);
                    
                    // Description
                    XmlElement description = document.CreateElement("description");
                    description.InnerText = plugin.Description;
                    xplugin.AppendChild(description);

                    // Assembly Qualified Name
                    XmlElement aqn = document.CreateElement("fname");
                    aqn.InnerText = plugin.GetType().AssemblyQualifiedName;
                    xplugin.AppendChild(aqn);

                    // Dependencies
                    XmlElement dependencies = document.CreateElement("dependencies");
                    foreach (string sDependency in plugin.PluginDependencies)
                    {
                        XmlElement dependency = document.CreateElement("dependency");
                        dependency.InnerText = sDependency;
                        dependencies.AppendChild(dependency);
                    }
                    xplugin.AppendChild(dependencies);

                    // Tibia Supported Versions
                    XmlElement supportedTibiaVersions = document.CreateElement("supportedTibiaVersions");
                    foreach (string sStv in plugin.SupportedTibiaVersions)
                    {
                        XmlElement stv = document.CreateElement("version");
                        stv.InnerText = sStv;
                        supportedTibiaVersions.AppendChild(stv);
                    }
                    xplugin.AppendChild(supportedTibiaVersions);

                    plugins.AppendChild(xplugin);

                    document.AppendChild(plugins["pending"]["install"]);

                    document.Save("TibiaTekPlus.Plugins.xml");

                    if (MessageBox.Show("The plug-in has been installed.\nIt will ready to use the next time you start TibiaTek Plus.\nWould you like to restart it now?", "Information", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        Application.Restart();
                    }
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

        private void webBrowser1_Navigating(object sender, WebBrowserNavigatingEventArgs e)
        {
            Uri url = e.Url;
            if (String.IsNullOrEmpty(url.Host))
                return;
            MessageBox.Show(url.Host);
            MessageBox.Show(url.Query);
            e.Cancel = true;
        }

        private void PluginManagerForm_Load(object sender, EventArgs e)
        {
            //StreamReader sw = new StreamReader(@"C:\page.html");
            //webBrowser2.DocumentStream = sw.BaseStream;
        }

        private void webBrowser2_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            HtmlDocument doc = webBrowser2.Document;

        }

        private void PluginManagerForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            this.Hide();
        }


    }
}
