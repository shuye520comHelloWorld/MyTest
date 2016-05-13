using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iParty.Services.ORM.Operations
{

    public class WorkShopUniteeOperation : BaseEntityOperation<WorkShopUniteeEnity>
    {
        public WorkShopUniteeOperation() : this(ConfigurationManager.AppSettings["Community"]) { }
        public WorkShopUniteeOperation(string dbStr) : base(dbStr) { }
    }

    public class WorkShopUniteeEnity
    {
        public int Recordid { get; set; }
        public long ContactId { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string LevelCode { get; set; }
        public string MobilePhone { get; set; }
        public DateTime StartDate { get; set; }
        public bool IsHost { get; set; }
        public bool IsApplied { get; set; }
        public string UnitId { get; set; }
    }
}
