using ServiceStack.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeautyContestTW.WindowsService.Entity.Entity
{
    [Alias("cn.RPT_DirectorRelationships")]
    public class DirectorRelationships
    {
        public DirectorRelationships() { }
        public int ProductionMonthKEY { get; set; }
        public long DirectorKey { get; set; }
        public long NSDKey { get; set; }
    }
}
