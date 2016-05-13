using ServiceStack.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iParty.Services.ORM.Operations
{
    public class CustomerListOperation : BaseEntityOperation<CustomerListEntity>
    {
        public CustomerListOperation() : this(ConfigurationManager.AppSettings["Community"]) { }
        public CustomerListOperation(string dbStr) : base(dbStr) { }
    }
    public sealed class CustomerListEntity
    {
        public Guid InvitationKey { get; set; }
        public Guid CustomerKey { get; set; }
        public Guid PartyKey { get; set; }
        public long? CustomerContactId { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public long OwnerContactID { get; set; }
        public string OwnerName { get; set; }
        public string Level { get; set; }
        public string Status { get; set; }
        public DateTime dtCreated { get; set; }
        public bool HasOrder { get; set; }
        public bool IsNewer { get; set; }
        public bool IsPromoted { get; set; }
    }
}
