using System;
using System.Windows.Forms;
using System.Xml;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using Tibia;
using Tibia.Util;
using TibiaTekPlus;
using TibiaTekPlus.Plugins;


namespace TibiaTekPlus
{
    public partial class Kernel : Plugins.IPluginHost
    {
        #region Events

        /// <summary>
        /// Prototype for pluginManager notifications.
        /// </summary>
        /// <param name="pluginManager"></param>
        public delegate void PluginNotification(Plugin plugin);

        /// <summary>
        /// Event fired when a pluginManager is loaded.
        /// </summary>
        public PluginNotification PluginLoaded;

        /// <summary>
        /// Prototype for skin notifications.
        /// </summary>
        /// <param name="skin"></param>
        public delegate void SkinNotification(Skin skin);

        /// <summary>
        /// Event fired after loading a skin successfully.
        /// </summary>
        public SkinNotification SkinLoaded;

        #endregion

        #region Forms

        /// <summary>
        /// Provides access to the Plug-in Manager form.
        /// </summary>
        public PluginManagerForm pluginsForm;

        /// <summary>
        /// Provides access to the About form.
        /// </summary>
        public AboutForm aboutForm;

        /// <summary>
        /// Provides access to the Options form.
        /// </summary>
        public OptionsForm optionsForm;

        #endregion

        #region Objects/Variables

        Tibia.Objects.Client client = null;
        public PluginCollection plugins;
        private string tibiaVersion = null;
        private Skin skin;
        private List<Skin> skins = new List<Skin>();

        #endregion

        /// <summary>
        /// Constructor of the kernel.
        /// </summary>
        public Kernel()
        {
            /* Instantiate forms */
            pluginsForm = new PluginManagerForm();
            aboutForm = new AboutForm();
            
            /* Instatiate timers */
            //timer = new Tibia.Util.Timer(3000, false);
            //timer.OnExecute += new Tibia.Util.Timer.TimerExecution(timer_OnExecute);

            /* Plug-in related */
            plugins = new PluginCollection();

            /* Skin related */
            //skin = new Skin(Settings.Default.Skin);
        }

        /// <summary>
        /// Initialize all the plugins.
        /// </summary>
        public void InitPlugins()
        {
            foreach (IPlugin plugin in plugins)
            {
                try
                {
                    plugin.Init();
                }
                catch (NotImplementedException)
                {
                    // Do nothing
                }
            }
        }

        /// <summary>
        /// Starts the kernel, enables the use of plug-ins. This function is called when the main form is ready.
        /// </summary>
        public void Enable()
        {
            foreach (IPlugin plugin in plugins)
            {
                if (plugin.State == PluginState.Running)
                {
                    try
                    {
                        plugin.Enable();
                    }
                    catch (NotImplementedException)
                    {
                        // Do nothing
                    }
                }
            }
        }

        /// <summary>
        /// Stops the kernel, stops all plug-ins currently running. This function is called when disconnected or exiting.
        /// </summary>
        public void Disable()
        {
            foreach (IPlugin plugin in plugins)
            {
                if (plugin.State == PluginState.Running)
                {
                    try
                    {
                        plugin.Disable();
                    }
                    catch (NotImplementedException)
                    {
                        // Do nothing
                    }
                }
            }
        }

        /// <summary>
        /// Pauses the kernel, all plug-ins running are paused
        /// </summary>
        public void Pause()
        {
            foreach (IPlugin plugin in plugins)
            {
                if (plugin.State == PluginState.Running)
                {
                    try
                    {
                        plugin.Pause();
                    }
                    catch (NotImplementedException)
                    {
                        // Do nothing
                    }
                }
            }
        }

        /// <summary>
        /// Resumes the kernel, all paused plug-ins are resumed
        /// </summary>
        public void Resume()
        {
            foreach (IPlugin plugin in plugins)
            {
                if (plugin.State == PluginState.Paused)
                {
                    try
                    {
                        plugin.Resume();
                    }
                    catch (NotImplementedException)
                    {
                        // Do nothing
                    }
                }
            }
        }

        /// <summary>
        /// Gets or sets a reference to the client object.
        /// </summary>
        public Tibia.Objects.Client Client
        {
            get
            {
                return client;
            }
            set
            {
                client = value;
                if (client != null)
                {
                    client.Exited += OnClientExit;
                }
            }
        }

        /// <summary>
        /// Uninstalls any pending plug-ins.
        /// </summary>
        /// <returns>Returns the number of uninstalled plug-ins.</returns>
        public int PerformPluginUninstallation()
        {
            int count = 0;
            XmlDocument document = new XmlDocument();
            document.Load(Path.Combine(Program.StartupPath,"TibiaTekPlus.Plugins.xml"));
            string filepath;
            foreach (XmlElement element in document["plugins"]["pending"]["uninstall"])
            {
                filepath = Path.Combine(Application.StartupPath, element.GetAttribute("fullname") + ".dll");
                try
                {
                    if (File.Exists(filepath))
                    {
                        File.SetAttributes(filepath, FileAttributes.Normal);
                        File.Delete(filepath);
                        document["plugins"]["pending"]["uninstall"].RemoveChild(element);
                        count++;
                    }
                } catch (Exception){
                }
            }
            document.Save(Path.Combine(Program.StartupPath,"TibiaTekPlus.Plugins.xml"));
            return count;
        }

        /// <summary>
        /// Install pending plug-ins.
        /// </summary>
        /// <returns>Returns the number of plug-ins installed.</returns>
        public int PerformPluginInstallation()
        {
            int count = 0;
            XmlDocument document = new XmlDocument();
            document.Load(Path.Combine(Program.StartupPath,"TibiaTekPlus.Plugins.xml"));
            string filepath;
            foreach (XmlElement element in document["plugins"]["pending"]["install"])
            {
                filepath = Path.Combine(Application.StartupPath, element.GetAttribute("fullname") + ".dll");
                if (File.Exists(filepath))
                {
                    XmlElement newelem = (XmlElement)element.Clone();
                    document["plugins"]["pending"]["install"].RemoveChild(element);
                    document["plugins"]["installed"].AppendChild(newelem);
                    count++;
                }
                else
                {
                    MessageBox.Show("Unable to install the following plug-in:\nTitle: " + element["title"] + ".\nAuthor: " + element["author"] + ".\nReason: The file '" + element.GetAttribute("fullname") + ".dll' was not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    document["plugins"]["pending"]["install"].RemoveChild(element);
                }
            }
            document.Save(Path.Combine(Program.StartupPath,"TibiaTekPlus.Plugins.xml"));
            return count;
        }

        /// <summary>
        /// Gets the number of plug-ins to be installed.
        /// </summary>
        public int InstalledPluginsCount
        {
            get
            {
                XmlDocument document = new XmlDocument();
                document.Load(Path.Combine(Program.StartupPath,"TibiaTekPlus.Plugins.xml"));
                return document["plugins"]["installed"].ChildNodes.Count;
            }
        }

        /// <summary>
        /// Loads the plug-ins into memory.
        /// </summary>
        /// <returns>Returns the number of loaded plug-ins.</returns>
        public int LoadPlugins()
        {
            int count = 0;
            XmlDocument document = new XmlDocument();
            document.Load(Path.Combine(Program.StartupPath, "TibiaTekPlus.Plugins.xml"));
            string path;
            foreach (XmlElement element in document["plugins"]["installed"])
            {
                try
                {
                    path = Path.Combine(Program.StartupPath, element.GetAttribute("fullname") + ".dll");
                    if (File.Exists(path))
                    {
                        Plugin plugin = (Plugin)Activator.CreateInstance(Type.GetType(element["assemblyQualifiedName"].InnerText));
                        plugin.Host = this;
                        plugins.Add(plugin);
                        count++;
                        if (PluginLoaded != null)
                        {
                            PluginLoaded.Invoke(plugin);
                        }
                    }
                    else
                    {
                        MessageBox.Show(String.Format(Language.kernel_error1, element["title"], element["author"], element.GetAttribute("fullname")), Language.Error, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception e)
                {
                    MessageBox.Show(String.Format(Language.kernel_error2, element.GetAttribute("fullname"), e.StackTrace + "\n" + e.Message), Language.Error, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            return count;
        }

        /// <summary>
        /// Gets or sets the current Tibia version
        /// </summary>
        public string TibiaVersion
        {
            get
            {
                return tibiaVersion;
            }
            set
            {
                tibiaVersion = value;
            }
        }

        /// <summary>
        /// Gets the plugins currently loaded.
        /// </summary>
        public PluginCollection Plugins
        {
            get
            {
                return plugins;
            }
        }

        public void OnClientExit(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        /// <summary>
        /// Gets the current skin.
        /// </summary>
        public Skin Skin
        {
            get
            {
                return skin;
            }
        }

        public Skin[] Skins
        {
            get
            {
                return skins.ToArray();
            }
        }


        /// <summary>
        /// Gets the number of installed skins.
        /// </summary>
        public int InstalledSkinsCount
        {
            get
            {
                SortedList skinlist = new SortedList();
                string defskinspath = Path.Combine(Program.StartupPath, "Skins");
                string userskinspath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), @"TibiaTek Plus\Skins");
                string[] defskins = Directory.GetDirectories(defskinspath);
                string[] userskins = new string[0];
                if (Directory.Exists(userskinspath))
                    userskins = Directory.GetDirectories(userskinspath);
                foreach (string dir in defskins)
                {
                    if (!skinlist.Contains(dir.Substring(defskinspath.Length + 1)))
                        skinlist.Add(dir.Substring(defskinspath.Length + 1), Path.Combine(defskinspath, dir.Substring(defskinspath.Length + 1)));
                }
                foreach (string dir in userskins)
                {
                    if (!skinlist.Contains(dir.Substring(userskinspath.Length + 1)))
                        skinlist.Add(dir.Substring(userskinspath.Length + 1), Path.Combine(userskinspath, dir.Substring(userskinspath.Length + 1)));
                    
                }
                return skinlist.Count;
            }
        }

        /// <summary>
        /// Loads available skins into memory.
        /// </summary>
        /// <returns>Returns the number of loaded skins.</returns>
        public int LoadSkins()
        {
            int count = 0;
            SortedList skinlist = new SortedList();
            string defskinspath = Path.Combine(Program.StartupPath, "Skins");
            string userskinspath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), @"TibiaTek Plus\Skins");
            string[] defskins = Directory.GetDirectories(defskinspath);
            string[] userskins = new string[0];
            if (Directory.Exists(userskinspath))
                userskins = Directory.GetDirectories(userskinspath);
            foreach (string dir in defskins)
            {
                if (!skinlist.Contains(dir.Substring(defskinspath.Length + 1)))
                    skinlist.Add(dir.Substring(defskinspath.Length + 1), Path.Combine(Program.StartupPath, @"Skins\" + dir.Substring(defskinspath.Length + 1)));
            }
            foreach (string dir in userskins)
            {
                if (!skinlist.Contains(dir.Substring(userskinspath.Length + 1)))
                skinlist.Add(dir.Substring(userskinspath.Length + 1), Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), @"TibiaTek Plus\Skins\" + dir.Substring(userskinspath.Length + 1)));
            }
            skins.Clear();
            foreach (DictionaryEntry skinentry in skinlist){
                try
                {
                    Skin s = new Skin((string)skinentry.Key, (string)skinentry.Value);
                    if (((string)skinentry.Key).Equals(Settings.Default.Skin))
                    {
                        skin = s;
                    }
                    skins.Add(s);
                    if (SkinLoaded != null)
                        SkinLoaded.Invoke(s);
                    count++;
                } catch(Exception ex){
                    MessageBox.Show(ex.Message, Language.Error, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            return count;
        }

    }

    /// <summary>
    /// Defines the states that the kernel can take.
    /// </summary>
    public enum KernelState
    {
        Stopped,
        Running,
        Paused
    }
}
