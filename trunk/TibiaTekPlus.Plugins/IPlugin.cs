using System;
using System.Drawing;
using System.Windows.Forms;
using System.Runtime.Serialization;
using System.Runtime.Remoting;

namespace TibiaTekPlus.Plugins
{
    public interface IPlugin
    {
        /// <summary>
        /// Load a configuration for this plug-in.
        /// </summary>
        /// <param name="path">Path to the configuration file</param>
        /// <returns>True if successfull, otherwise false</returns>
        bool Load(string path);

        /// <summary>
        /// Save the configuration for this plug-in.
        /// </summary>
        /// <param name="path">Path to the configuration file.</param>
        /// <returns>True if successfull, otherwise false.</returns>
        bool Save(string path);

        /// <summary>
        /// Show the main window of this plug-in.
        /// </summary>
        void Show();

        /// <summary>
        /// Hide the main window of this plug-in.
        /// </summary>
        void Hide();

        /// <summary>
        /// Gets the icon of this plug-in (stored in {PluginName}.Resources.Icon.ico).
        /// </summary>
        Icon Icon { get; }

        /// <summary>
        /// Gets the title of this plug-in.
        /// </summary>
        string Title{ get; }

        /// <summary>
        /// Gets the author of this plug-in.
        /// </summary>
        string Author { get; }

        /// <summary>
        /// Gets the version of this plug-in.
        /// </summary>
        string Version { get; }

        /// <summary>
        /// Gets the description of this plug-in.
        /// </summary>
        string Description { get; }

        /// <summary>
        /// Gets an array of strings containing the supported Tibia versions of this plug-in.
        /// </summary>
        string[] SupportedTibiaVersions { get; }

        /// <summary>
        /// Gets an array of strings containing this plug-in's dependencies. Format of each string: FullName, Filename.
        /// </summary>
        string[] PluginDependencies { get; }

        /// <summary>
        /// Gets the main form of this plug-in, if any.
        /// </summary>
        Form MainForm { get; }

        /// <summary>
        /// Gets the collection of forms whithin this plug-in.
        /// </summary>
        FormCollection Forms { get; }

        /// <summary>
        /// Gets the version of the kernel this plug-in supports. Format: "M\.m\.b\.r".
        /// Where M stands for Major version, m for Minor version, b for Build version, and r for Revision number.
        /// It must be a valid regular expression. Example: "1\.0\.\d+\.\d+".
        /// </summary>
        string SupportedKernel { get; }

        /// <summary>
        /// Gets the state of this plug-in.
        /// </summary>
        PluginState State { get; set; }

        /// <summary>
        /// Starts the plug-in.
        /// </summary>
        void Enable();

        /// <summary>
        /// Stops the plug-in.
        /// </summary>
        void Disable();

        /// <summary>
        /// Pauses the plug-in if running.
        /// </summary>
        void Pause();

        /// <summary>
        /// Resumes the plug-in if paused.
        /// </summary>
        void Resume();

        /// <summary>
        /// Gets the visibility of the plug-in. Plug-ins that work in the background should be invisible.
        /// </summary>
        bool Visible { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether or not the plug-in can interact with the kernel and/or user.
        /// </summary>
        bool Enabled { get; }
    }

    /// <summary>
    /// Defines the plug-in states.
    /// </summary>
    public enum PluginState
    {
        Stopped,
        Running,
        Paused
    }
}
