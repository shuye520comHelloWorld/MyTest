using iParty.Services.Entity;
using ServiceStack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iParty.Services.Contract
{
    [Api("query the customer detail info.")]
    [Route("/parties/customer/{InvitationKey}", "GET, OPTIONS")]
    public sealed class CustomerDetail : CommandRequestDto, IReturn<CustomerDetailResponse>, IPublicRequestDto
    {
        [ApiMember(Name = "InvitationKey", ParameterType = "path", Description = "the unique identifier of the party invitation", DataType = "guid", IsRequired = true)]
        public Guid InvitationKey { get; set; }
    }

    public sealed class CustomerDetailResponse : CommandResponseDto
    {
        public Guid InvitationKey { get; set; }
        public Guid CustomerKey { get; set; }
        public Guid PartyKey { get; set; }
        public string FullName { get; set; }
        public string PhoneNumber { get; set; }
        public bool IsVIP { get; set; }
        public long OwnerContactID { get; set; }
        public string OwnerName { get; set; }
        [ApiMember(Name = "Status", ParameterType = "body", Description = "the status of the invitation for the customer, 00-Accepted, 01-CheckedIn, 03-Cancelled, 04-OnSite", DataType = "string")]
        public string Status { get; set; }
        [ApiMember(Name = "Career", ParameterType = "body", Description = "the career of the customer", DataType = "enum")]
        public Career? Career { get; set; }
        [ApiMember(Name = "Age", ParameterType = "body", Description = "the age range of the customer", DataType = "enum")]
        public AgeRange? AgeRange { get; set; }
        [ApiMember(Name = "MaritalStatus", ParameterType = "body", Description = "the marital status of the customer", DataType = "enum")]
        public MaritalStatusType? MaritalStatus { get; set; }
        public DateTime dtCreated { get; set; }
    }
}
