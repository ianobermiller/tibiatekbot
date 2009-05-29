using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TibiaTekPlus
{
    public partial class Kernel
    {
        #region Objects/Variables

        private Tibia.Packets.ProxyBase proxy = null;

        #endregion

        // proxy stuff here
        public Tibia.Packets.ProxyBase Proxy
        {
            get {
                if (proxy == null)
                    proxy = new Tibia.Packets.HookProxy(client);
                return proxy;
            }
            set
            {
                proxy = value;
            }
        }
    }
}
