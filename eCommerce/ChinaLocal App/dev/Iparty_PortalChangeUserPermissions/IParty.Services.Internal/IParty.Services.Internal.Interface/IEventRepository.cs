using IParty.Services.Internal.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IParty.Services.Internal.Interface
{
    public interface IEventRepository
    {
        GetEventResponse GetEvent(GetEvent dto);
    }
}
