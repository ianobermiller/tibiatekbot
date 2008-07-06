using System;
using System.IO;
using System.Xml;
using System.Text;

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

        /// <summary>
        /// Constructor for the Skin class.
        /// </summary>
        /// <param name="skinName">Name of the skin to load.</param>
        public Skin(string skinName){
            string relpath = @"Skins\" + skinName;
            string absolutepath = System.IO.Path.Combine(Directory.GetCurrentDirectory(),relpath);
            string documentspath = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)+@"\TibiaTek Plus", relpath);
            if (Directory.Exists(absolutepath)) {
                path = absolutepath;
            }
            else if (Directory.Exists(documentspath))
            {
                path = documentspath;
            }
            else
            {
                throw new DirectoryNotFoundException("Unable to locate skin. Make sure it is installed in the following location:\n" + documentspath + ".");
            }
            XmlDocument document = new XmlDocument();
            document.Load(System.IO.Path.Combine(path, String.Format("{0}.xml", skinName)));
            XmlElement skinelem = document["skin"];
            name = skinelem["name"].InnerText;
            version = skinelem["version"].InnerText;
            description = skinelem["description"].InnerText;
            author = skinelem["author"].InnerText;
            email = skinelem["email"].InnerText;
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
