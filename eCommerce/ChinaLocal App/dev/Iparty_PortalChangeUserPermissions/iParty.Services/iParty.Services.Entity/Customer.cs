using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iParty.Services.Entity
{
    /// <summary>
    /// the customer info who is invited into a party
    /// </summary>
    public  class Customer
    {
        public Guid CustomerKey { get; set; }
        public String Name { get; set; }
        public String PhoneNumber { get; set; }
        // 
        public Career Career { get; set; }
        // 
        public AgeRange AgeRange { get; set; }
        public bool IsVIP { get; set; }
        public MaritalStatusType MaritalStatus { get; set; } 

        
    }
}
