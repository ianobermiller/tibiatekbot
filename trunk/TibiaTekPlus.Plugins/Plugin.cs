using System;
using System.Collections.Generic;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;
using System.Runtime.Serialization;
using System.Security;
using System.Security.Permissions;
using TibiaTekPlus.Plugins;

namespace TibiaTekPlus.Plugins
{
    public abstract class Plugin : MarshalByRefObject , IPlugin 
    {
        public delegate void PluginStateChanged(PluginState newState, PluginState oldState);

        public PluginStateChanged StateChanged;

        private PluginState state = PluginState.Stopped;
        private bool visible = false;

        /// <summary>
        /// Load a configuration for this plug-in.
        /// </summary>
        /// <param name="path">Path to the configuration file</param>
        /// <returns>True if successfull, otherwise false</returns>
        bool IPlugin.Load(string path)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Save the configuration for this plug-in.
        /// </summary>
        /// <param name="path">Path to the configuration file.</param>
        /// <returns>True if successfull, otherwise false.</returns>
        bool IPlugin.Save(string path)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Show the main window of this plug-in.
        /// </summary>
        void IPlugin.Show()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Hide the main window of this plug-in.
        /// </summary>
        void IPlugin.Hide()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Gets the icon of this plug-in (stored in {PluginName}.Resources.Icon.ico).
        /// </summary>
        Icon IPlugin.Icon
        {
            get
            {
                Assembly a = Assembly.GetExecutingAssembly();
                System.IO.Stream s = a.GetManifestResourceStream(string.Format("{0}.{1}", a.GetName(), "Resources.Icon.ico"));
                return new Icon(s);
            }
        }

        /// <summary>
        /// Gets the title of this plug-in.
        /// </summary>
        string IPlugin.Title
        {
            get
            {
                Assembly assembly = Assembly.GetExecutingAssembly();
                AssemblyTitleAttribute title = (AssemblyTitleAttribute)Attribute.GetCustomAttribute(assembly,typeof(AssemblyTitleAttribute));
                return title.Title;
            }
        }

        /// <summary>
        /// Gets the author of this plug-in.
        /// </summary>
        string IPlugin.Author
        {
            get
            {
                Assembly assembly = Assembly.GetExecutingAssembly();
                AssemblyCompanyAttribute company = (AssemblyCompanyAttribute)Attribute.GetCustomAttribute(assembly, typeof(AssemblyCompanyAttribute));
                return company.Company;  
            }
        }

        /// <summary>
        /// Gets the version of this plug-in.
        /// </summary>
        string IPlugin.Version
        {
            get
            {
                Assembly assembly = Assembly.GetExecutingAssembly();
                AssemblyVersionAttribute version = (AssemblyVersionAttribute)Attribute.GetCustomAttribute(assembly, typeof(AssemblyVersionAttribute));
                return version.Version; 
            }
        }

        /// <summary>
        /// Gets the description of this plug-in.
        /// </summary>
        string IPlugin.Description
        {
            get
            {
                Assembly assembly = Assembly.GetExecutingAssembly();
                AssemblyDescriptionAttribute description = (AssemblyDescriptionAttribute)Attribute.GetCustomAttribute(assembly, typeof(AssemblyDescriptionAttribute));
                return description.Description;
            }
        }

        /// <summary>
        /// Gets an array of strings containing the supported Tibia versions of this plug-in.
        /// </summary>
        string[] IPlugin.SupportedTibiaVersions
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        /// <summary>
        /// Gets an array of strings containing this plug-in's dependencies. Format of each string: FullName, Filename.
        /// </summary>
        string[] IPlugin.PluginDependencies
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        /// <summary>
        /// Gets the main form of this plug-in, if any.
        /// </summary>
        Form IPlugin.MainForm
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        /// <summary>
        /// Gets the collection of forms whithin this plug-in.
        /// </summary>
        FormCollection IPlugin.Forms
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        /// <summary>
        /// Gets the version of the kernel this plug-in supports. Format: "M\.m\.b\.r".
        /// Where M stands for Major version, m for Minor version, b for Build version, and r for Revision number.
        /// It must be a valid regular expression. Example: "1\.0\.\d+\.\d+".
        /// </summary>
        string IPlugin.SupportedKernel
        {
            get
            {
                return @"\d+.\d+.\d+.\d+";
            }
        }

        /// <summary>
        /// Gets or sets the state of this plug-in.
        /// </summary>
        PluginState IPlugin.State
        {
            get
            {
                return state;
            }
            set
            {
                if (value != state && StateChanged != null)
                {
                    StateChanged.Invoke(value, state);
                }
                state = value;
            }
        }

        /// <summary>
        /// Starts the plug-in.
        /// </summary>
        void IPlugin.Start()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Stops the plug-in.
        /// </summary>
        void IPlugin.Stop()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Pauses the plug-in if running.
        /// </summary>
        void IPlugin.Pause()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Resumes the plug-in if paused.
        /// </summary>
        void IPlugin.Resume()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Gets the visibility of the plug-in. Plug-ins that work in the background should be invisible.
        /// </summary>
        bool IPlugin.Visible
        {
            get
            {
                return visible;
            }
            set
            {
                visible = value;
            }
        }
    }
}
