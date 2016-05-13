using ServiceStack.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iParty.Services.ORM.Operations
{
    public class WorkShopOperation : BaseEntityOperation<WorkShopEntity>
    {
        public WorkShopOperation() : this(ConfigurationManager.AppSettings["Community"]) { }
        public WorkShopOperation(string dbStr) : base(dbStr) { }
    }
    
    public class WorkShopEntity
    {
        public int Recordid { get; set; }
        public long ContactId { get; set; }
        public string ShopType { get; set; }
        public int TypeValue { get; set; }
        public string Province { get; set; }
        public string City { get; set; }
        public string County { get; set; }
        public string ShopAddress { get; set; }
        public string ShopZipCode { get; set; }
        public string ContactTel { get; set; }
        public string FixedTel { get; set; }
        public string ShopLicenseName { get; set; }
    }
}
