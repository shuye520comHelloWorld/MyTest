using BeautyContestTW.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeautyContestTW.WindowsService.Entity.Operations
{
  
    public class SeasonConfigOperation : BaseEntityOperation<SeasonConfig>
    {
        public SeasonConfigOperation(string dbStr) : base(dbStr) { }
        public SeasonConfig GetCurrentSeason()
        {
            var today = DateTime.Now.Date;
            return base.GetSingleData(m => m.StartDateTime.Date <= today && m.EndDateTime >= today);
        }
    }
}
