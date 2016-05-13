using ServiceStack.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IParty.Services.Entity
{
    /// <summary>
    /// Event information
    /// </summary>
    [Alias("PB_Event")]
    public class Event
    {
        /// <summary>
        /// The identifier of event
        /// </summary>
        [PrimaryKey]
        public Guid EventKey { get; set; }

        public int Category { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public DateTime ApplicationStartDate { get; set; }

        public DateTime ApplicationEndDate { get; set; }

        public DateTime EventStartDate { get; set; }

        public DateTime EventEndDate { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime? PartyAllowStartDate { get; set; }

        public DateTime? PartyAllowEndDate { get; set; }

        public DateTime? FeedbackEndDate { get; set; } 

    }
}
