using System;
using System.Collections.Generic;

namespace iParty.Services.Entity
{
    public class Workshop
    {
        public int WorkshopId { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public WorkshopType Type { get; set; }
        public IEnumerable<CoHostConsultant> Consultants { get; set; }
    }
}
