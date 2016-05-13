using iParty.Services.Entity;
using QuartES.Services.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iParty.Services.Interface
{
    public interface IEventsProvider
    {
        IEnumerable<EventEntry> GetBy();
        IEnumerable<EventEntryWithDetail> GetDetailBy(Guid eventKey);
        void GetNotAppliedEvents(List<object> partyList);
    }

    public class EventEntry
    {
        public Guid EventKey { get; set; }
        public string Title { get; set; }
        public PartyCategory Category { get; set; }
        public DateTime ApplicationStartDate { get; set; }
        public DateTime ApplicationEndDate { get; set; }
        public DateTime EventStartDate { get; set; }
        public DateTime EventEndDate { get; set; }
        public string Description { get; set; }
        public DateTime PartyAllowEndDate { get; set; }
        public DateTime PartyAllowStartDate { get; set; }
    }

    public class EventEntryWithDetail : EventEntry
    { 
        public IEnumerable<string> Notes { get; set; } 
    }
}
