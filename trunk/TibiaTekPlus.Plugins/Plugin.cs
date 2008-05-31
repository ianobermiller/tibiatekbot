using System;
using System.Collections.Generic;
using System.Drawing;
using System.Reflection;
using TibiaTekPlus.Plugins;

namespace TibiaTekPlus.Plugins
{
    public abstract class Plugin : IPlugin
    {

        public abstract bool Load(string path);

        public abstract bool Save(string path);

        public virtual void Show()
        {
            throw new NotImplementedException();
        }

        public virtual void Hide()
        {
            throw new NotImplementedException();
        }

        public string Title
        {
            get
            {
                Assembly assembly = Assembly.GetExecutingAssembly();
                AssemblyTitleAttribute title = (AssemblyTitleAttribute)Attribute.GetCustomAttribute(assembly,typeof(AssemblyTitleAttribute));
                return title.Title;
            }
        }

        public Icon Icon
        {
            get
            {
                Assembly a = Assembly.GetExecutingAssembly();
                System.IO.Stream s = a.GetManifestResourceStream(string.Format("{0}.{1}", a.GetName(),"Resources.Icon.ico"));
                return new Icon(s);
            }
        }

        public string Author
        {
            get
            {
                Assembly assembly = Assembly.GetExecutingAssembly();
                AssemblyCompanyAttribute company = (AssemblyCompanyAttribute)Attribute.GetCustomAttribute(assembly, typeof(AssemblyCompanyAttribute));
                return company.Company;  
            }
        }

        public string Version
        {
            get
            {
                Assembly assembly = Assembly.GetExecutingAssembly();
                AssemblyVersionAttribute version = (AssemblyVersionAttribute)Attribute.GetCustomAttribute(assembly, typeof(AssemblyVersionAttribute));
                return version.Version; 
            }
        }

        public virtual string[] SupportedTibiaVersions
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public virtual string[] PluginDependencies
        {
            get
            {
                throw new NotImplementedException();
            }
        }
    }
}
