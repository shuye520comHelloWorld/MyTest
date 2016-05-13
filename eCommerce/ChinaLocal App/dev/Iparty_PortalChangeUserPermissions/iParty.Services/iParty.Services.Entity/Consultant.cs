using System;

namespace iParty.Services.Entity
{
    public class Consultant
    {
        public long ContactId { get; set; }
        public int Level { get; set; }
        public DateTime StartDate { get; set; }
        public string FullName { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string UnitId { get; set; }
    }

    public class CoHostConsultant : Consultant
    {
        public bool IsOwner { get; set; }
        public bool IsApplied { get; set; }
    }
}
