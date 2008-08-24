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

            // Uninstall pending plug-ins
            kernel.PerformPluginUninstallation();
            splashScreenForm.Percent = 5;

            // Install pending plug-ins
            kernel.PerformPluginInstallation();
            splashScreenForm.Percent = 10;

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
            options.LookUpClients = false;
            options.Smart = false;

            kernel.Client = Tibia.Util.ClientChooserWPF.ShowBox(options);
            if (kernel.Client == null) Environment.Exit(0);

            kernel.Client.OnExit += (Tibia.Objects.Client.ClientNotification)kernel.OnClientExit;
            kernel.Client.Process.WaitForInputIdle();
            kernel.Client.StartProxy();
            // Create the main form
            mainForm = new MainForm();
            
            // Enable the kernel (initialization prior to main window)
            kernel.Enable();
            ApplicationContext appContext = new ApplicationContext(mainForm);
            appContext.ThreadExit += new EventHandler(OnApplicationExit);
            Application.Run(appContext);
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
