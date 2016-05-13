using BeautyContestTW.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeautyContestTW.WindowsService.Entity.Operations
{
    public class TicketOperation : BaseEntityOperation<Ticket>
    {
        public TicketOperation(string dbStr) : base(dbStr) { }
    }

    public class TicketInvalidOperation : BaseEntityOperation<TicketInvalid>
    {
        public TicketInvalidOperation(string dbStr) : base(dbStr) { }
    }
}
