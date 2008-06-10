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
        private bool enabled = false;

        /// <summary>
        /// Load a configuration for this plug-in.
        /// </summary>
        /// <param name="path">Path to the configuration file</param>
        /// <returns>True if successfull, otherwise false</returns>
        public virtual bool Load(string path)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Save the configuration for this plug-in.
        /// </summary>
        /// <param name="path">Path to the configuration file.</param>
        /// <returns>True if successfull, otherwise false.</returns>
        public virtual bool Save(string path)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Show the main window of this plug-in.
        /// </summary>
        public virtual void Show()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Hide the main window of this plug-in.
        /// </summary>
        public virtual void Hide()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Gets the icon of this plug-in (stored in {PluginName}.Resources.Icon.ico).
        /// </summary>
        public Icon Icon
        {
            get
            {
                Assembly a = Assembly.GetAssembly(this.GetType());
                System.IO.Stream s = a.GetManifestResourceStream(string.Format("{0}.{1}", a.GetName(), "Resources.Icon.ico"));
                return new Icon(s);
            }
        }

        /// <summary>
        /// Gets the title of this plug-in.
        /// </summary>
        public string Title
        {
            get
            {
                Assembly assembly = Assembly.GetAssembly(this.GetType());
                AssemblyTitleAttribute title = (AssemblyTitleAttribute)Attribute.GetCustomAttribute(assembly,typeof(AssemblyTitleAttribute));
                return title.Title;
            }
        }

        /// <summary>
        /// Gets the author of this plug-in.
        /// </summary>
        public string Author
        {
            get
            {
                Assembly assembly = Assembly.GetAssembly(this.GetType());
                AssemblyCompanyAttribute company = (AssemblyCompanyAttribute)Attribute.GetCustomAttribute(assembly, typeof(AssemblyCompanyAttribute));
                return company.Company;  
            }
        }

        /// <summary>
        /// Gets the version of this plug-in.
        /// </summary>
        public string Version
        {
            get
            {
                Assembly assembly = Assembly.GetAssembly(this.GetType());
                AssemblyFileVersionAttribute version = (AssemblyFileVersionAttribute)Attribute.GetCustomAttribute(assembly, typeof(AssemblyFileVersionAttribute));
                return version.Version; 
            }
        }

        /// <summary>
        /// Gets the description of this plug-in.
        /// </summary>
        public string Description
        {
            get
            {
                Assembly assembly = Assembly.GetAssembly(this.GetType());
                AssemblyDescriptionAttribute description = (AssemblyDescriptionAttribute)Attribute.GetCustomAttribute(assembly, typeof(AssemblyDescriptionAttribute));
                return description.Description;
            }
        }

        /// <summary>
        /// Gets the category of this plug-in.
        /// </summary>
        public virtual string Category
        {
            get
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// Gets an array of strings containing the supported Tibia versions of this plug-in.
        /// </summary>
        public virtual string[] SupportedTibiaVersions
        {
            get
            {
                return new string[] { };
            }
        }

        /// <summary>
        /// Gets an array of strings containing this plug-in's dependencies. Format of each string: FullName, Filename.
        /// </summary>
        public virtual string[] PluginDependencies
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        /// <summary>
        /// Gets the main form of this plug-in, if any.
        /// </summary>
        public virtual Form MainForm
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        /// <summary>
        /// Gets the collection of forms whithin this plug-in.
        /// </summary>
        public virtual FormCollection Forms
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
        public virtual string SupportedKernel
        {
            get
            {
                return @"\d+.\d+.\d+.\d+";
            }
        }

        /// <summary>
        /// Gets or sets the state of this plug-in.
        /// </summary>
        public PluginState State
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
        public virtual void Enable()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Stops the plug-in.
        /// </summary>
        public virtual void Disable()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Pauses the plug-in if running.
        /// </summary>
        public virtual void Pause()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Resumes the plug-in if paused.
        /// </summary>
        public virtual void Resume()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Gets the visibility of the plug-in. Plug-ins that work in the background should be invisible.
        /// </summary>
        public bool Visible
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

        /// <summary>
        /// Gets a value indicating whether or not the plug-in can interact with the kernel and/or user.
        /// </summary>
        public bool Enabled
        {
            get
            {
                return enabled;
            }
        }
    }
}
