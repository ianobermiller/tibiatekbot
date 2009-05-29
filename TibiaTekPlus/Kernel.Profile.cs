using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using TibiaTekPlus.Plugins;

namespace TibiaTekPlus
{
    public partial class Kernel
    {
        #region Objects / Variables

        private Profile defaultProfile;
        private Profile currentProfile;

        #endregion

        public IProfile DefaultProfile
        {
            get
            {
                if (defaultProfile == null)

                return defaultProfile;
            }
            set
            {
                defaultProfile = (Profile)value;
            }
        }


        /// <summary>
        /// Loads the current profile from disk.
        /// </summary>
        public void LoadDefaultProfile(string path)
        {
                defaultProfile = new Profile(string.IsNullOrEmpty(path)?Path.Combine(Program.StartupPath, "TibiaTekPlus.Profile.xml"):path);
        }

        /// <summary>
        /// Saves the current profile to disk.
        /// </summary>
        public void SaveProfile(string path)
        {
            defaultProfile.Save(string.IsNullOrEmpty(path)?Path.Combine(Program.StartupPath, "TibiaTekPlus.Profile.xml"):path);
        }
    }
}
