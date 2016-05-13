using BeautyContestTW.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeautyContestTW.WindowsService.Entity.Operations
{

    public class RGOrderOperation : BaseEntityOperation<Order>
    {
        public RGOrderOperation(string dbStr) : base(dbStr) { }
    }



    public class RGOrderPartsOperation : BaseEntityOperation<OrderParts>
    {
        public RGOrderPartsOperation(string dbStr) : base(dbStr) { }
    }

    

}
