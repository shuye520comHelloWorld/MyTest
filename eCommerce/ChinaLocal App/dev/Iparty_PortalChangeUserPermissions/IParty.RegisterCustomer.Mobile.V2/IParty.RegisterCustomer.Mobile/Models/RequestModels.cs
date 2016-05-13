using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IParty.RegisterCustomer.Mobile.Models
{

    public class PartyInfo
    {
        public Guid PartyKey { get; set; }
        public string EventTitle { get; set; }
        public string WorkshopLocation { get; set; }
        public DateTime PartyStartDate { get; set; }
        public DateTime PartyEndDate { get; set; }
        public DateTime ApplicationStartDate { get; set; }
        public DateTime ApplicationEndDate { get; set; }

        public string meetingtime { get; set; }
        public long ForwordContactID { get; set; }
        public string ForwordName { get; set; }

        public ResponseStatus ResponseStatus { get; set; }
    }

    public class cardId
    {
        public Guid EventKey { get; set; }
        public string CardID { get; set; }
        public DateTime CreateDate { get; set; }
        public ResponseStatus ResponseStatus { get; set; }

    }

    public class InvitationResponse
    {
        public string InvitationKey { get; set; }
        public string CustomerKey { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string Career { get; set; }
        public string Age { get; set; }
        public bool IsVIP { get; set; }
        public string MaritalStatus { get; set; }
        public string InvationQRKey { get; set; }
        public ResponseStatus ResponseStatus { get; set; }
    }

    public class WechatCard
    {
        public Guid CardCode { get; set; }
        public string CardID { get; set; }
        public Guid PartyKey { get; set; }
        public int Status { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime UpdateDate { get; set; }
        public ResponseStatus ResponseStatus { get; set; }
    }

    public class ResponseStatus {
        public string ErrorCode { get; set; }
        public string Message { get; set; }
        public string[] Errors { get; set; }
    }
}