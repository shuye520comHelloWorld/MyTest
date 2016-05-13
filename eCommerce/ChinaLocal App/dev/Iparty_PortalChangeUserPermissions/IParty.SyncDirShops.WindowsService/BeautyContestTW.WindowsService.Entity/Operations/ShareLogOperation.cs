using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BeautyContestTW.Entity.Entities;

namespace BeautyContestTW.WindowsService.Entity.Operations
{
    public class ShareLogOperation : BaseEntityOperation<ShareLog>
    {
        public ShareLogOperation(string dbStr) : base(dbStr) { } 
    }
}
