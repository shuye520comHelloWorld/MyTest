using PinkBus.Services.Internal.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PinkBus.Services.Internal.Interface
{
    public interface IEventRepository
    {
        GetEventResponse GetEvent(GetEvent dto);

    }
}
