using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace TibiaTekPlus
{

    static class Program
    {
        /// <summary>
        /// Provides access to the Kernel object.
        /// </summary>
        static public Kernel kernel;

        /// <summary>
        /// Provides access to the main form.
        /// </summary>
        static public MainForm mainForm;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Show timed splash screen
            SplashScreenForm ssf = new SplashScreenForm();
            ssf.ShowDialog();

            // Create the main form
            mainForm = new MainForm();

            // Instantiate the kernel object
            kernel = new Kernel();

            // Start the kernel (initialization prior to main window)
            kernel.Start();

            ApplicationContext appContext = new ApplicationContext(mainForm);
            appContext.ThreadExit += new EventHandler(OnApplicationExit);
            Application.Run(appContext);
        }

        static private void OnApplicationExit(object sender, EventArgs e)
        {
            // Save settings here?
            
            //global::TibiaTekPlus.Properties.Settings.Default
        }

    }
}
