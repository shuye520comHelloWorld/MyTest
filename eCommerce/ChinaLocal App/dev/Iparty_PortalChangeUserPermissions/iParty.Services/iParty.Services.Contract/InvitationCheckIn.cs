using iParty.Services.Entity;
using ServiceStack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iParty.Services.Contract
{
    [Api("the check in api.")]
    [Route("/parties/invitations/checkin", "PUT, OPTIONS")]
    public sealed class InvitationCheckIn : CommandRequestDto, IReturn<CheckInResponse>
    {
        [ApiMember(Name = "InvitationKey", ParameterType = "body", Description = "the Invitation primary Key", DataType = "guid", IsRequired = true)]
        public Guid InvitationKey { get; set; }
        [ApiMember(Name = "CheckInType", ParameterType = "body", Description = "the check in type, 00-WechartWithCard, 01-Browser, 02-Album, 03-Letter, 04-Intouch, 05-WechartNoCard", DataType = "string", IsRequired = true)]
        public string CheckInType { get; set; }
        [ApiMember(Name = "CheckInDate", ParameterType = "body", Description = "the check inTime", DataType = "datetime", IsRequired = true)]
        public DateTime CheckInDate { get; set; }
    }
    public sealed class CheckInResponse : CommandResponseDto
    {
        public Guid InvitationKey { get; set; }
        public Guid CustomerKey { get; set; }
        public String Name { get; set; }
        public String PhoneNumber { get; set; }
        public Career? Career { get; set; }
        public AgeRange? Age { get; set; }
        public bool IsVIP { get; set; }
        public string Level { get; set; }
        public MaritalStatusType? MaritalStatus { get; set; }
    }
}
