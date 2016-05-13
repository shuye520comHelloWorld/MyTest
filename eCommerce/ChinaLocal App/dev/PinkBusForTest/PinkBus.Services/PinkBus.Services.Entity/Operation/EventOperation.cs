using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PinkBus.Services.Entity.Operation
{
    public class EventOperation : BaseEntityOperation<Event>
    {
        public EventOperation(string strConn)
            : base(strConn)
        {

        }
    }
}
