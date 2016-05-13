using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PinkBus.EventStat.Models
{
    public class SearchModel
    {
        public int Page { get; set; }
        public int pageSize { get; set; }
        public string TableName { get; set; }
        public string Field { get; set; }
        public string OrderBy { get; set; }
        public string Filter { get; set; }
        public int MaxPage { get; set; }
        public int TotalRow { get; set; }
        public string Descript { get; set; }
    }

    public class CheckinUser
    {
        public Guid? CustomerKey { get; set; }
        public Guid? MappingKey { get; set; }
        public string DirectSellerId { get; set; }
        public CustomerType CustomerType { get; set; }
        public AgeRange? AgeRange { get; set; }
        public Career? Career { get; set; }
        public bool? BeautyClass { get; set; }
        public bool? UsedProduct { get; set; }
        public string InterestingTopic { get; set; }
        public string UsedSet { get; set; }
        public string InterestInCompany { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string CustomerName { get; set; }
        public string CustomerPhone { get; set; }
        public TicketType TicketType { get; set; }
        public TicketStatus? TicketStatus { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? CheckinDate { get; set; }
        public string UpdatedBy { get; set; }

    }

    public class CheckinSummary
    {
        public int NormalCheckinCount { get; set; }
        public int NormalCount { get; set; }
        public int VIPCheckinCount { get; set; }
        public int VIPCount { get; set; }
        public int VolunteerCheckinCount { get; set; }
        public int VolunteerCount { get; set; }
    }

    public class IndexEvent
    {
        public Guid EventKey { get; set; }
        public string EventTitle { get; set; }
        public DateTime EventStartDate { get; set; }

        public DateTime EventEndDate { get; set; }
        public DateTime ApplyTicketStartDate { get; set; }
        public DateTime ApplyTicketEndDate { get; set; }
        public DateTime CheckinStartDate { get; set; }
        public int NormalTicketCount { get; set; }
        public int VIPTicketCount { get; set; }
        public int InviterCount { get; set; }
        public int CheckinCount { get; set; }
        public int NeedCheckinCount { get; set; }
        public int NewCustomerCount { get; set; }
    }
}