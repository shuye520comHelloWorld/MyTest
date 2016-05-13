using BeautyContestTW.Entity.Entities;
using BeautyContestTW.WindowsService.Entity.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeautyContestTW.WindowsService.Entity.Operations
{

    public class OEOrderWithPartsOperation : BaseEntityOperation<OrderWithParts>
    {
        public OEOrderWithPartsOperation(string dbStr) : base(dbStr) { }
    }
}
