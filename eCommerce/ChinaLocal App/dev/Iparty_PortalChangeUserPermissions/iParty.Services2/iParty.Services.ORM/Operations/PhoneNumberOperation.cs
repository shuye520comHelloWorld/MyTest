using ServiceStack.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iParty.Services.ORM.Operations
{
    public class PhoneNumberOperation : BaseEntityOperation<PhoneNumberEntity>
    {
        public PhoneNumberOperation() : this(ConfigurationManager.AppSettings["ContactsLite"]) { }
        public PhoneNumberOperation(string dbStr) : base(dbStr) { }
    }

    [Alias("PhoneNumbers")]
    public class PhoneNumberEntity
    {
        public long PhoneNumberID { get; set; }
        public long ContactID { get; set; }
        public short PhoneNumberTypeID { get; set; }
        public string PhoneNumber { get; set; }
        public bool IsPrimary { get; set; }
    }

    public class PhoneNumberInfo
    {
        public long ContactID { get; set; }
        public string PhoneNumber { get; set; }
        public string UnitID { get; set; }
    }
}
