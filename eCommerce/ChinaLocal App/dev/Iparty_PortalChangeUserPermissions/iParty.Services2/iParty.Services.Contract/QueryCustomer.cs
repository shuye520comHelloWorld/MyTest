using iParty.Services.Entity;
using ServiceStack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iParty.Services.Contract
{
    [Api("query the customer list of a party.")]
    [Route("/parties/{PartyKey}/customers", "GET, OPTIONS")]
    public sealed class QueryCustomer : CommandRequestDto, IReturn<QueryCustomerResponse>
    {
        [ApiMember(Name = "PartyKey", ParameterType = "path", Description = "the unique identifier of the party", DataType = "guid", IsRequired = true)]
        public Guid PartyKey { get; set; }
    }

    public sealed class QueryCustomerResponse : CommandResponseDto
    {
        public Guid InvitationKey { get; set; }
        public Guid CustomerKey { get; set; }
        public Guid PartyKey { get; set; }
        public long? ContactId { get; set; }
        public string FullName { get; set; }
        public string PhoneNumber { get; set; }
        public long OwnerContactID { get; set; }
        public string OwnerName { get; set; }
        public string LevelOnAccept { get; set; }
        [ApiMember(Name = "Status", ParameterType = "body", Description = "the status of the invitation for the customer, 00-Accepted, 01-CheckedIn, 03-Cancelled, 04-OnSite", DataType = "string", IsRequired = true)]
        public string Status { get; set; }
        public bool IsNewer { get; set; }
        public bool IsPromoted { get; set; }
        public bool HasOrder { get; set; }
        public DateTime dtCreated { get; set; }
    }
}
