using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.IO;

namespace TibiaTekPlus
{
    public class Profile
    {
        Dictionary<string, string> settings;

        public Profile()
        {
            settings = new Dictionary<string, string>();
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
                XmlElement profile = document["profile"];

                foreach (XmlElement settingNode in profile)
                {
                    Set(UnescapeXml(settingNode.GetAttribute("plugin")),
                        UnescapeXml(settingNode.GetAttribute("key")),
                        UnescapeXml(settingNode.InnerText));
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
                string pluginName = setting.Key.Substring(0, index);
                string key = setting.Key.Substring(index + 1);
                XmlElement settingNode = document.CreateElement("setting");

                XmlAttribute pluginNameAttr = document.CreateAttribute("plugin");
                pluginNameAttr.InnerText = EscapeXml(pluginName);
                settingNode.Attributes.Append(pluginNameAttr);

                XmlAttribute keyAttr = document.CreateAttribute("key");
                keyAttr.InnerText = EscapeXml(key);
                settingNode.Attributes.Append(keyAttr);

                settingNode.InnerText = EscapeXml(setting.Value);

                profile.AppendChild(settingNode);
            }
            document.Save(path);
        }

        public string Get(string pluginName, string key)
        {
            string dictKey = pluginName + "." + key;

            if (settings.ContainsKey(dictKey))
            {
                return settings[dictKey];
            }
            
            return string.Empty;
        }

        public void Set(string pluginName, string key, string value)
        {
            settings.Add(pluginName + "." + key, value);
        }

        public static string EscapeXml(string s)
        {
            string xml = s;
            if (!string.IsNullOrEmpty(xml))
            {
                // replace literal values with entities
                xml = xml.Replace("&", "&amp;");
                xml = xml.Replace("<", "&lt;");
                xml = xml.Replace(">", "&gt;");
                xml = xml.Replace("\"", "&quot;");
                xml = xml.Replace("'", "&apos;");
            }
            return xml;
        }

        public static string UnescapeXml(string s)
        {
            string unxml = s;
            if (!string.IsNullOrEmpty(unxml))
            {
                // replace entities with literal values
                unxml = unxml.Replace("&apos;", "'");
                unxml = unxml.Replace("&quot;", "\"");
                unxml = unxml.Replace("&gt;", "&gt;");
                unxml = unxml.Replace("&lt;", "&lt;");
                unxml = unxml.Replace("&amp;", "&");
            }
            return unxml;
        }

    }
}
