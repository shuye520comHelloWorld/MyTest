using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PinkBus.Services.Infrastructure.Test.Common;
namespace PinkBus.Services.Infrastructure.Test
{
    public class PinkBusHelper : Helper
    {
        public PinkBusHelper(bool enableOAuth, EnvType envType, HostType hostType)
            : base(enableOAuth, envType, hostType)
        {
        }

         

        public PinkBusHelper()
        {
           // base.client
        }
    }
}
