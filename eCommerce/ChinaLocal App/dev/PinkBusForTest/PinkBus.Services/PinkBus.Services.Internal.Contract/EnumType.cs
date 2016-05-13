using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PinkBus.Services.Internal.Contract
{
    public enum TicketStatus
    {
        UnUsed,
        Used,
        Invalid
    }

    public enum TicketType
    {
        VIP,
        Normal
    }
}
