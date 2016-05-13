using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PinkBus.CheckInClient.Entitys
{
    public class ConsultantsWhichHasUnUseTicket
    {
        public Guid MappingKey { get; set; }
        public Guid EventKey { get; set; }
        public string ContactId { get; set; }
        public string DirectSellerId { get; set; }
        public int UserType { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public int Count { get; set; }
        public int TicketType { get; set; }

        public string Name { get { return LastName + FirstName; } }
    }

    public class CustomerTicket : Customer
    {

        public TicketType TicketType { get; set; }
        public TicketStatus TicketStatus { get; set; }
        public string TicketKey { get; set; }
        public string SMSToken { get; set; }
        public new Guid CustomerKey { get; set; }
        public string UserType { get { return TicketType == TicketType.Normal ? "来宾" : (TicketType == TicketType.VIP ? "贵宾" : "志愿者"); } }
        public string UserName { get { return CustomerName; } }
        public string UserPhoneNumber { get { return CustomerPhone; } }
        public int? CheckinCount { get; set; }
        public string CheckinStatus { get { return TicketStatus == TicketStatus.Checkin || CheckinCount.HasValue ? "已签到" + (CheckinCount.HasValue && CheckinCount > 0 ? "(" + CheckinCount.Value + ")" : "") : (TicketStatus == TicketStatus.Canceled ? "已作废" : "未签到"); } set { } }

    }

    public class ConsultantTicket : Consultant
    {
        public int? CheckinCount { get; set; }
    }


    public class CustomerInfo : Customer
    {
        public TicketStatus TicketStatus { get; set; }
        public TicketType TicketType { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string PhoneNumber { get; set; }
        public string ConsultantName { get { return LastName + FirstName; } }
        public string ConsultantPhone { get { return PhoneNumber; } }

    }


    public class VolunteerForExcel
    {
        public Guid EventKey { get; set; }
        public Guid MappingKey { get; set; }
        public string DirectSellerId { get; set; }
        public string ConsultantName { get; set; }
        public string ConsultantPhone { get; set; }
        public string ConsultantLevel { get; set; }
        public string Province { get; set; }
        public string City { get; set; }
        public string County { get; set; }
        public DateTime CheckinDate { get; set; }
        public DateTime CheckinDay { get; set; }
        
    }


    public class OauthTokenModel
    {
        public string access_token { get; set; }
        public string token_type { get; set; }
        public int expires_in { get; set; }
        public string scope { get; set; }
        public string ErrorCode { get; set; }
    }
    public class ConsultantImageModel
    {
        public long image_id { get; set; }
        public string image_data { get; set; }
        public string image_format { get; set; }
        public string error_code { get; set; }
        public string error_msg { get; set; }
    }


}
