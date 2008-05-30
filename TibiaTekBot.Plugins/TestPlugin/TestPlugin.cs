using System;
using System.Windows.Forms;
using System.Drawing;
using System.Reflection;
using TibiaTekPlus.Plugins;
using TestPlugin;

namespace TibiaTekPlus.Plugins
{
    public class TestPlugin : TibiaTekPlus.Plugins.Plugin 
    {
        MainForm mainForm;
        private string[] supportedVersions = { "8.11" };

        public TestPlugin()
        {
            mainForm = new MainForm();
        }

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

        public override void Show()
        {
            mainForm.Show();
        }

        public override void Hide()
        {
            mainForm.Hide();
        }

        public void Open()
        {
            if (mainForm == null)
                mainForm = new MainForm();
            mainForm.Show();
        }

        public void Close()
        {
            mainForm.Close();
        }

        public override string[] SupportedTibiaVersions
        {
            get
            {
                return supportedVersions;
            }
        }

        public override string[] PluginDependencies
        {
            get { 
                throw new NotImplementedException();
            }
        }

    }
}
