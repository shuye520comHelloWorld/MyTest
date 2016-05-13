using ServiceStack.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iParty.Services.ORM.Operations
{
     

    public class EventDetailOperation : BaseEntityOperation<EventDetailEntity>
    {
        public EventDetailOperation() : this(ConfigurationManager.AppSettings["Community"]) { }
        public EventDetailOperation(string dbStr) : base(dbStr) { }
    }
    [Alias("iParty_EventDetail")]
    public class EventDetailEntity
    {
        public Guid EventKey { get; set; } 
        public string Note { get; set; }  
    }
}
