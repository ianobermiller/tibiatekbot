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

        static private int step=0;

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

            if (kernel.InstalledPluginsCount > 0)
            {
                step = 90 / kernel.InstalledPluginsCount;

                kernel.LoadPlugins();
            }
            else
            {
                splashScreenForm.Percent = 100;
            }
            Application.DoEvents();
            System.Threading.Thread.Sleep(1000);

            splashScreenForm.Close();

            // Gets the client
            kernel.Client = Tibia.Util.ClientChooserWPF.ShowBox();
            if (kernel.Client == null) Environment.Exit(0);

            kernel.Client.OnExit += (Tibia.Objects.Client.ClientNotification)kernel.OnClientExit;

            // Create the main form
            mainForm = new MainForm();

            // Enable the kernel (initialization prior to main window)
            kernel.Enable();

            ApplicationContext appContext = new ApplicationContext(mainForm);
            appContext.ThreadExit += new EventHandler(OnApplicationExit);
            Application.Run(appContext);
        }

        static public Application Application
        {
            get { return Application;  }
        }

        static private void kernel_PluginLoaded(Plugin plugin)
        {
            if (splashScreenForm.Percent + step <= 100)
            {
                splashScreenForm.Percent += step;
            }
            else
            {
                splashScreenForm.Percent = 100;
            }
            // Avoid freezing
            Application.DoEvents();
        }

        static private void OnApplicationExit(object sender, EventArgs e)
        {
            // Close the client
            if (kernel.Client != null)
                kernel.Client.Close();

            // Save settings on graceful exit
            Settings.Default.Save();
        }

    }
}
