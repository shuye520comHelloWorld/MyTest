using ServiceStack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iParty.Services.Contract
{
    [Api("check a customer whether belong to the units of CoHostConsultants.")]
    [Route("/parties/invitations/customer", "POST, OPTIONS")]
    public sealed class CheckCustomerInvitation : CommandRequestDto, IReturn<CheckCustomerResponse>, IPublicRequestDto
    {
        [ApiMember(Name = "InvitationToken", ParameterType = "body", Description = "the InvitationToken", DataType = "string", IsRequired = true)]
        public string InvitationToken { get; set; }
        [ApiMember(Name = "PhoneNumber", ParameterType = "body", Description = "the mobile phone number of the customer", DataType = "string", IsRequired = true)]
        public string PhoneNumber { get; set; }
    }

    public sealed class CheckCustomerResponse : CommandResponseDto
    {
        public long? CustomerContactID { get; set; }
        public int? Level { get; set; }
        public long? DIRContactID { get; set; }
        public string DIRName { get; set; }
        public long? RecuiterContactID { get; set; }
        public string RecuiterName { get; set; }
        public long? OwnerContactID { get; set; }
        public string UnitID { get; set; }
        public bool IsVIP { get; set; }
        public string Message { get; set; }
    }
}
