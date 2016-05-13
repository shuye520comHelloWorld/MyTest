
using ServiceStack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PinkBus.Services.Common;
namespace PinkBus.Services.Contract
{

    [Api("get the ticket detail infomation when customer open the invitation link in H5 page with wechat")]
    [Route("/customer/invitation/query", "GET, OPTIONS")]
    public class QueryInvitation : QueryRequestDto, IReturn<QueryInvitationResponse>
    {
        [ApiMember(Name = "TicketKey", ParameterType = "body", Description = "the unqiue identifier of TicketKey ", DataType = "Guid", IsRequired = true)]
        public Guid TicketKey { get; set; }
    }

    public class QueryInvitationResponse : QueryResponseDto
    {
        [ApiMember(Name = "EventBaseInfo", ParameterType = "body", DataType = "object", Description = "including current event concrete information", IsRequired = true)]
        public EventBaseInfo EventBaseInfo { get; set; }

        [ApiMember(Name = "TicketKey", ParameterType = "body", DataType = "guid", Description = "the unique identifier of Ticket", IsRequired = true)]
        public Guid TicketKey { get; set; }

        [ApiMember(Name = "TicketType", ParameterType = "body", DataType = "enum", Description = "ticket type,variable are normal,vip", IsRequired = true)]
        public TicketType TicketType { get; set; }

        [ApiMember(Name = "TicketStatus", ParameterType = "body", DataType = "enum", Description = "ticket status,variable are Created,Inviting,Invited,Bestowed,Canceled, Checkin,UnCheckin,Expired", IsRequired = true)]
        public TicketStatus TicketStatus { get; set; }
              
        [ApiMember(Name = "FirstName", ParameterType = "body", Description = "the first name of the consultant", DataType = "string", IsRequired = true)]
        public string FirstName { get; set; }

        [ApiMember(Name = "LastName", ParameterType = "body", Description = "the last name of the consultant", DataType = "string", IsRequired = true)]
        public string LastName { get; set; }

        [ApiMember(Name = "ConsultantPhone", ParameterType = "body", DataType = "string", Description = "the phone number of inviter", IsRequired = true)]
        public string ConsultantPhone { get; set; }

        [ApiMember(Name = "WechatUserType", ParameterType = "body", DataType = "enum", Description = "the identity type of use who open the h5 page ,variable are Inviter/Customer/Other", IsRequired = true)]
        public WechatUserType WechatUserType { get; set; }

        [ApiMember(Name = "IsInvitationEnd", ParameterType = "body", DataType = "bool", Description = "indicate the time is expired than the event invitation end time", IsRequired = false)]
        public bool IsInvitationEnd { get; set; }

        [ApiMember(Name = "CustomerName", ParameterType = "body", DataType = "string", Description = "customer name", IsRequired = false)]
        public string CustomerName { get; set; }

        [ApiMember(Name = "HeadImgUrl", ParameterType = "body", DataType = "string", Description = "wechart user head image url ", IsRequired = false)]
        public string HeadImgUrl { get; set; }


    }
}
