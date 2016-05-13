using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PinkBus.Services.Entity.Operation
{
    public class OfflineCustomerOperation : BaseEntityOperation<OfflineCustomer>
    {
        public OfflineCustomerOperation(string strConn)
            : base(strConn)
        {

        }
    }
}
