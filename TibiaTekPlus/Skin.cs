using System;
using System.IO;
using System.Xml;
using System.Text;
using System.Collections.Generic;
using System.Windows.Forms;

namespace TibiaTekPlus
{
    public class Skin
    {
        private string name = "";
        private string author = "";
        private string description = "";
        private string version = "";
        private string email = "";
        private string path = "";
        public string item = "";
        /// <summary>
        /// Constructor for the Skin class.
        /// </summary>
        /// <param name="skinName">Name of the skin to load.</param>
        public Skin(string skinName, string path)
        {
            if (!Directory.Exists(path)) {
                throw new DirectoryNotFoundException("Unable to locate skin. Make sure it is installed in the following location:\n" + path + ".");
            }
            XmlDocument document = new XmlDocument();
            document.Load(System.IO.Path.Combine(path, String.Format("{0}.xml", skinName)));
            XmlElement skinelem = document["skin"];
            name = skinelem["name"].InnerText;
            version = skinelem["version"].InnerText;
            description = skinelem["description"].InnerText;
            author = skinelem["author"].InnerText;
            email = skinelem["email"].InnerText;
            this.path = path;
            XmlElement files = skinelem["files"];
            string filename = String.Empty;
            foreach (XmlElement file in files)
            {
                filename = System.IO.Path.Combine(path, file.InnerText);
                if (!File.Exists(filename)) {
                    throw new FileNotFoundException(String.Format(Language.Skin_MissingSkinFile, skinName, filename));
                }
            }
            if (skinName.Equals("Default"))
            {
                item = skinelem["item"].InnerXml;
            }
        }

        /// <summary>
        /// Gets the name of this skin.
        /// </summary>
        public string Name
        {
            get
            {
                return name;
            }
        }

        /// <summary>
        /// Gets this skin's author.
        /// </summary>
        public string Author
        {
            get
            {
                return author;
            }
        }

        /// <summary>
        /// Gets the email of the skin's author.
        /// </summary>
        public string Email
        {
            get
            {
                return email;
            }
        }

        /// <summary>
        /// Gets the version of this skin.
        /// </summary>
        public string Version
        {
            get
            {
                return version;
            }
        }

        /// <summary>
        /// Gets the description of this plug-in.
        /// </summary>
        public string Description
        {
            get
            {
                return description;
            }
        }

        /// <summary>
        /// Gets the path of this skin.
        /// </summary>
        public string Path
        {
            get
            {
                return path;
            }
        }
    }
}
