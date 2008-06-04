using System;
using System.Windows.Forms;
using Tibia;
using Tibia.Util;
using TibiaTekPlus;
using TibiaTekPlus.Plugins;



namespace TibiaTekPlus
{
    public partial class Kernel
    {
        #region Forms

        /// <summary>
        /// Provides access to the Plug-ins Form
        /// </summary>
        public PluginManagerForm pluginsForm;

        #endregion

        #region Objects/Variables

        Tibia.Objects.Client client;
        public Tibia.Util.Timer timer;
        public PluginCollection plugins;

        #endregion

        /// <summary>
        /// Constructor of the kernel.
        /// </summary>
        public Kernel()
        {
            /* Instantiate forms */
            pluginsForm = new PluginManagerForm();

            /* Instatiate timers */
            timer = new Tibia.Util.Timer(3000, false);
            timer.OnExecute += new Tibia.Util.Timer.TimerExecution(timer_OnExecute);

            /* Plug-in related */
            plugins = new PluginCollection();
        }

        /// <summary>
        /// Starts the kernel, enables the use of plug-ins. This function is called when the main form is ready.
        /// </summary>
        public void Enable()
        {
            foreach (IPlugin plugin in plugins)
            {
                if (plugin.State == PluginState.Running)
                {
                    try
                    {
                        plugin.Enable();
                    }
                    catch (NotImplementedException)
                    {
                        // Do nothing
                    }
                }
            }
        }

        /// <summary>
        /// Stops the kernel, stops all plug-ins currently running. This function is called when disconnected or exiting.
        /// </summary>
        public void Disable()
        {
            foreach (IPlugin plugin in plugins)
            {
                if (plugin.State == PluginState.Running)
                {
                    try
                    {
                        plugin.Disable();
                    }
                    catch (NotImplementedException)
                    {
                        // Do nothing
                    }
                }
            }
        }

        /// <summary>
        /// Pauses the kernel, all plug-ins running are paused
        /// </summary>
        public void Pause()
        {
            foreach (IPlugin plugin in plugins)
            {
                if (plugin.State == PluginState.Running)
                {
                    try
                    {
                        plugin.Pause();
                    }
                    catch (NotImplementedException)
                    {
                        // Do nothing
                    }
                }
            }
        }

        /// <summary>
        /// Resumes the kernel, all paused plug-ins are resumed
        /// </summary>
        public void Resume()
        {
            foreach (IPlugin plugin in plugins)
            {
                if (plugin.State == PluginState.Paused)
                {
                    try
                    {
                        plugin.Resume();
                    }
                    catch (NotImplementedException)
                    {
                        // Do nothing
                    }
                }
            }
        }

        /// <summary>
        /// Gets a reference to the client object.
        /// </summary>
        public Tibia.Objects.Client Client
        {
            get
            {
                return client;
            }
        }

    }

    /// <summary>
    /// Defines the states that the kernel can take.
    /// </summary>
    public enum KernelState
    {
        Stopped,
        Running,
        Paused
    }
}
