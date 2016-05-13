using ServiceStack.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeautyContestTW.WindowsService.Entity.Entity
{

   public class MyConsultants
    {
        public long ContactID { get; set; }
        public string ConsultantStatus { get; set; }
        public int ConsultantLevelID { get; set; }     
        public decimal UnitProduction { get; set; }
        public decimal Production { get; set; }
    }
}
