using System;
using System.Drawing;

namespace TibiaTekPlus.Plugins
{
    public interface IPlugin
    {
        bool Load(string path);
        bool Save(string path);
        void Show();
        void Hide();
        Icon Icon { get; }
        string Title{ get; }
        string[] SupportedTibiaVersions { get; }
        string Version { get; }
        string Author { get; }
        string[] PluginDependencies { get; }
    }

}
