using BeautyContestTW.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeautyContestTW.WindowsService.Entity.Operations
{
    public class LotteryNotWinningOperation : BaseEntityOperation<LotteryNotWinnning>
    {
        public LotteryNotWinningOperation(string dbStr) : base(dbStr) { }
    }
}
