using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PinkBus.Services.Entity.Operation
{
    public class TicketOperation : BaseEntityOperation<Ticket>
    {
        public TicketOperation(string strConn)
            : base(strConn)
        {

        }
    }
}
