using BeautyContestTW.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeautyContestTW.WindowsService.Entity.Entity
{
    public class OrderWithParts
    {
        public Guid OrderId { get; set; }
        public long PurchaserContactID { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalCost { get; set; }
        public int PurchaserCareerLevelID { get; set; }

        public  List<OrderParts> OrderPartsArray { get; set; }
    }
}
