using System;
using System.Windows.Forms;
using System.Drawing;
using System.Reflection;
using TibiaTekPlus.Plugins;
using Tibia;
using TestPlugin;

namespace TibiaTekPlus.Plugins
{
    public class TestPlugin : TibiaTekPlus.Plugins.Plugin 
    {
        MainForm mainForm;
        private string[] supportedVersions = { "8.11" };
        private string supportedKernel = @"1\.\d+\.\d+\.\d+";

        Tibia.Util.Timer timer;

        #region Initialization/Finalization

        public TestPlugin()
        {
            mainForm = new MainForm();
            timer = new Tibia.Util.Timer(3000, false);
        }
        ~TestPlugin()
        {
        }

        #endregion

        #region Configuration Settings

        public override bool Load(string path)
        {
            MessageBox.Show("Finished loading.");
            return true;
        }
        public override bool Save(string path)
        {
            MessageBox.Show("Finished saving.");
            return true;
        }

        #endregion

        #region Graphic User Interface

        public override void Show()
        {
            if (mainForm != null)
                mainForm.Show();
        }
        public override void Hide()
        {
            if (mainForm != null)
                mainForm.Hide();
        }
        public override Form MainForm
        {
            get
            {
                return mainForm;
            }
        }

        #endregion

        #region Dependencies & Support

        public override string[] SupportedTibiaVersions
        {
            get
            {
                return supportedVersions;
            }
        }
        public override string[] PluginDependencies
        {
            get
            { 
                return new string[] {};
            }
        }
        public override string SupportedKernel
        {
            get
            {
                return supportedKernel;
            }
        }

        #endregion

        
    }
}
