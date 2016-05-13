using ServiceStack.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iParty.Services.ORM.Operations
{
    public class CustomerDetailOperation : BaseEntityOperation<CustomerDetailEntity>
    {
        public CustomerDetailOperation() : this(ConfigurationManager.AppSettings["Community"]) { }
        public CustomerDetailOperation(string dbStr) : base(dbStr) { }
    }

    public sealed class CustomerDetailEntity
    {
        public Guid InvitationKey { get; set; }
        public Guid CustomerKey { get; set; }
        public Guid PartyKey { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public long OwnerContactID { get; set; }
        public string OwnerName { get; set; }
        public string Status { get; set; }
        public int? Career { get; set; }
        public int? AgeRange { get; set; }
        public int? MaritalStatus { get; set; }
        public bool IsVIP { get; set; }
        public DateTime dtCreated { get; set; }
    }
}
