using ServiceStack.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iParty.Services.ORM.Operations
{

    public class EventOperation : BaseEntityOperation<EventEntity>
    {
        public EventOperation() : this(ConfigurationManager.AppSettings["Community"]) { }
        public EventOperation(string dbStr) : base(dbStr) { }
    }
    [Alias("iParty_Event")]
    public class EventEntity
    {
        public Guid EventKey { get; set; }
        public string Title { get; set; }
        /// <summary>
        ///  1 Love,
        ///  2 HighendVIP,
        /// </summary>
        public int Category { get; set; }
        public DateTime ApplicationStartDate { get; set; }
        public DateTime ApplicationEndDate { get; set; }
        public string Description { get; set; }
        public DateTime EventStartDate { get; set; }
        public DateTime EventEndDate { get; set; }
        public DateTime PartyAllowStartDate { get; set; }
        public DateTime PartyAllowEndDate { get; set; }
        public DateTime? FeedbackEndDate { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
