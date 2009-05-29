using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.IO;
using TibiaTekPlus.Plugins;

namespace TibiaTekPlus
{
    public class Profile : IProfile
    {
        Dictionary<string, string> settings = new Dictionary<string, string>();

        public Profile()
        {
        }

        public Profile(string path)
            : this()
        {
            Load(path);
        }

        public void Load(string path)
        {
            if (File.Exists(path))
            {
                XmlDocument document = new XmlDocument();
                document.Load(path);
                foreach (XmlElement settingNode in document["profile"])
                {
                    string pluginName = settingNode.GetAttribute("plugin");
                    string key = settingNode.GetAttribute("key");
                    Set(pluginName + "." + key, settingNode.InnerText);
                }
            }

        }

        public void Save(string path)
        {
            XmlDocument document = new XmlDocument();
            XmlElement profile;
            if (File.Exists(path))
            {
                document.Load(path);
                profile = document["profile"];
            }
            else
            {
                XmlDeclaration declaration = document.CreateXmlDeclaration("1.0", "", "");
                document.AppendChild(declaration);
                profile = document.CreateElement("profile");
                document.AppendChild(profile);
            }
            foreach (KeyValuePair<string, string> setting in settings)
            {
                int index = setting.Key.IndexOf(".");
                string pluginName;
                string key;
                if (index == -1)
                {
                    pluginName = string.Empty;
                    key = setting.Key;
                }
                else
                {
                    pluginName = setting.Key.Substring(0, index);
                    key = setting.Key.Substring(index + 1);
                }
                XmlElement settingNode = document.CreateElement("setting");

                XmlAttribute pluginNameAttr = document.CreateAttribute("plugin");
                pluginNameAttr.InnerText = pluginName;
                settingNode.Attributes.Append(pluginNameAttr);

                XmlAttribute keyAttr = document.CreateAttribute("key");
                keyAttr.InnerText = key;
                settingNode.Attributes.Append(keyAttr);

                settingNode.InnerText = setting.Value;

                profile.AppendChild(settingNode);
            }
            document.Save(path);
        }

        /// <summary>
        /// Determins whether the given key exists in the current profile.
        /// </summary>
        /// <param name="key"></param>
        /// <returns>Returns true if the given key exists in the current profile, false otherwise.</returns>
        public bool HasKey(string key)
        {
            return settings.ContainsKey(key);
        }

        /// <summary>
        /// Determins whether the given profile and key combination exists in the current profile.
        /// </summary>
        /// <param name="plugin"></param>
        /// <param name="key"></param>
        /// <returns>Returns true if the given plugin and key combination exists in the current profile, false otherwise.</returns>
        public bool HasKey(IPlugin plugin, string key)
        {
            return HasKey(plugin.Title + "." + key);
        }

        /// <summary>
        /// Gets a plugin value given a plugin and a key.
        /// </summary>
        /// <param name="plugin">Plugin for which the key is set.</param>
        /// <param name="key">Key to look for.</param>
        /// <returns>Returns the corresponding value for the given Plugin and key combination if it exists, otherwise returns an empty string.</returns>
        public string Get(IPlugin plugin, string key)
        {
            string output = string.Empty;
            if (settings.TryGetValue(plugin.Title + "." + key, out output))
                return output;
            else
                return string.Empty;
        }

        /// <summary>
        /// Gets the specified key from the current profile.
        /// </summary>
        /// <param name="key">Key to look for.</param>
        /// <returns>Returns the value for the given key if found, otherwise returns an empty string.</returns>
        public string Get(string key)
        {
            string output = string.Empty;
            if (settings.TryGetValue(key, out output))
                return output;
            else
                return string.Empty;
        }

        /// <summary>
        /// Sets the specified key given the Plugin and key name.
        /// </summary>
        /// <param name="plugin">Reference to the plugin setting the value.</param>
        /// <param name="key">Key to be set.</param>
        /// <param name="value">Value of the key to be set</param>
        public void Set(IPlugin plugin, string key, string value)
        {
            var dictKey = plugin.Title + "." + key;
            lock (settings)
            {
                if (!settings.ContainsKey(dictKey))
                    settings.Add(dictKey, value);
                else
                    settings[dictKey] = value;
            }
        }

        /// <summary>
        /// Sets a value given a key.
        /// </summary>
        /// <param name="key">Key to be set.</param>
        /// <param name="value">Value of the key to be set.</param>
        public void Set(string key, string value)
        {
            lock (settings)
            {
                if (!settings.ContainsKey(key))
                    settings.Add(key, value);
                else
                    settings[key] = value;
            }
        }

    }
}
