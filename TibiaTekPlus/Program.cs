using System;
using System.Collections.Generic;
using TibiaTekPlus.Plugins;
using System.Windows.Forms;

namespace TibiaTekPlus
{

    static class Program
    {

        /// <summary>
        /// Instance of the Kernel object.
        /// </summary>
        static public Kernel kernel;

        /// <summary>
        /// Instance of the main form.
        /// </summary>
        static public MainForm mainForm;

        

        /// <summary>
        /// Instance of the splash screen form;
        /// </summary>
        static private SplashScreenForm splashScreenForm;

        static private int pluginstep = 0;
        static private int skinstep = 0;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Instantiate the kernel object
            kernel = new Kernel();
            kernel.PluginLoaded += new Kernel.PluginNotification(kernel_PluginLoaded);
            kernel.SkinLoaded += new Kernel.SkinNotification(kernel_SkinLoaded);

            // Show splash screen
            splashScreenForm = new SplashScreenForm();
            splashScreenForm.Show();
            splashScreenForm.Percent = 0;

            // Load profile
            kernel.LoadDefaultProfile(null);
            splashScreenForm.Percent = 5;

            // Uninstall pending plug-ins
            kernel.PerformPluginUninstallation();
            splashScreenForm.Percent = 10;

            // Install pending plug-ins
            kernel.PerformPluginInstallation();
            splashScreenForm.Percent = 15;

            // Load skins
            if (kernel.InstalledSkinsCount == 0)
            {
                MessageBox.Show(Language.Program_NoSkins, Language.Error, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            skinstep = (int)Math.Floor((double)30 / kernel.InstalledSkinsCount);
            kernel.LoadSkins();
            
            if (kernel.InstalledPluginsCount > 0)
            {
                pluginstep = (int)Math.Floor((double)60 / kernel.InstalledPluginsCount);

                kernel.LoadPlugins();
            }
            else
            {
                splashScreenForm.Percent = 100;
            }
            
            splashScreenForm.Close();

            // Gets the client
            
            Tibia.Util.ClientChooserOptions options = new Tibia.Util.ClientChooserOptions();
            options.LookUpClients = true;
            options.Smart = false;

            kernel.Client = Tibia.Util.ClientChooserWPF.ShowBox(options);
            if (kernel.Client == null) Environment.Exit(0);

            kernel.Client.Process.WaitForInputIdle();
            kernel.Client.Window.Title = string.Format(Kernel.TitleFormat, ProductName, Language.mainForm_Title_Loading);
            kernel.Proxy = new Tibia.Packets.HookProxy(kernel.Client);
            kernel.Proxy.ReceivedSelfAppearIncomingPacket += new Tibia.Packets.ProxyBase.IncomingPacketListener(proxy_ReceivedSelfAppearIncomingPacket);
            //kernel.Client.StartProxy();
            // Create the main form
            mainForm = new MainForm();
            mainForm.Shown += new EventHandler(mainForm_Shown);            
            // Enable the kernel (initialization prior to main window)
            kernel.Enable();
            ApplicationContext appContext = new ApplicationContext(mainForm);
            appContext.ThreadExit += new EventHandler(OnApplicationExit);
            Application.Run(appContext);
        }

        static void mainForm_Shown(object sender, EventArgs e)
        {
            if (!kernel.Client.LoggedIn)
            {
                kernel.Client.Window.Title = string.Format(Kernel.TitleFormat, ProductName, Language.mainForm_Title_NotLoggedIn);
            }
            else
            {
                Tibia.Objects.Player p = kernel.Client.GetPlayer();
                if (p != null)
                    kernel.Client.Window.Title = string.Format(Kernel.TitleFormat, ProductName, p.Name);
            }
        }

        static bool proxy_ReceivedSelfAppearIncomingPacket(Tibia.Packets.IncomingPacket packet)
        {
            Tibia.Objects.Player p = kernel.Client.GetPlayer();
            if (p == null)
                throw new Exception("Unable to set the Tibia client title, not logged in.");
            kernel.Client.Window.Title = string.Format(Kernel.TitleFormat, ProductName, p.Name);
            return true;
        }

        /// <summary>
        /// Returns the product name of this application.
        /// </summary>
        static public string ProductName
        {
            get { return Application.ProductName; }
        }

        /// <summary>
        /// Returns the current version of this application.
        /// </summary>
        static public string ProductVersion
        {
            get { return Application.ProductVersion; }
        }

        static public String StartupPath
        {
            get { return Application.StartupPath;  }
        }

        static private void kernel_PluginLoaded(Plugin plugin)
        {
            if (splashScreenForm.Percent + pluginstep <= 100)
            {
                splashScreenForm.Percent += pluginstep;
            }
            else
            {
                splashScreenForm.Percent = 100;
            }
        }

        static private void kernel_SkinLoaded(Skin skin)
        {
            if (splashScreenForm.Percent + skinstep <= 40)
            {
                splashScreenForm.Percent += skinstep;
            }
            else
            {
                splashScreenForm.Percent = 40;
            }
        }

        static private void OnLoggedIn()
        {

        }

        static private void OnApplicationExit(object sender, EventArgs e)
        {
            // Save settings on graceful exit
            Settings.Default.Save();

            // Close the client
            if (kernel.Client != null)
                kernel.Client.Close();


        }

    }
}
