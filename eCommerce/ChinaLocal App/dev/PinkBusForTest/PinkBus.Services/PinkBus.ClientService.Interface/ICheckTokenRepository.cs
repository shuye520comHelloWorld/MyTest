using PinkBus.ClientServices.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PinkBus.ClientServices.Interface
{
    public interface ICheckTokenRepository
    {
        CheckTokenResponse CheckToken(CheckToken dto);
    }
}
