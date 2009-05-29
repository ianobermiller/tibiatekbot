using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TibiaTekPlus.Plugins
{
    public interface IProfile
    {

        void Load(string path);

        void Save(string path);

        bool HasKey(string key);
        bool HasKey(IPlugin plugin, string key);

        string Get(string key);
        string Get(IPlugin plugin, string key);

        void Set(string key, string value);
        void Set(IPlugin plugin, string key, string value);

    }
}
