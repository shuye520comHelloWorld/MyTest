using ServiceStack.DataAnnotations;
using ServiceStack.OrmLite;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iParty.Services.ORM.Operations
{
    public class ConsultantOperation : BaseEntityOperation<ConsultantEntity>
    {
        public ConsultantOperation() : this(ConfigurationManager.AppSettings["ContactsLite"]) { }
        public ConsultantOperation(string dbStr) : base(dbStr) { }
    }

    [Alias("Consultants")]
    public class ConsultantEntity
    {
        public long ContactID { get; set; }
        [Alias("ConsultantLevelID")]
        public int Level { get; set; }
        public DateTime StartDate { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string UnitID { get; set; }
    }

    public class CustomerBCOperation : BaseEntityOperation<CustomerBCEntity>
    {
        public CustomerBCOperation() : this(ConfigurationManager.AppSettings["ContactsLite"]) { }
        public CustomerBCOperation(string dbStr) : base(dbStr) { }
    }

    public class CustomerBCEntity
    {
        public long ContactID { get; set; }
        [Alias("ConsultantLevelID")]
        public int Level { get; set; }
        public string UnitID { get; set; }
        public string PhoneNumber { get; set; }
        public long? RecruiterContactID { get; set; }
        public string RecruiterName { get; set; }
        public long? DIRContactID { get; set; }
        public string DIRName { get; set; }
    }
}
