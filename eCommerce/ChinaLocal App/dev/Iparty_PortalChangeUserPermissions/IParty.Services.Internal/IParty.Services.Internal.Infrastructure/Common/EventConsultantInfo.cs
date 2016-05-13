using ServiceStack.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IParty.Services.Internal.Infrastructure
{
   public class EventConsultantInfo
   {
       /// <summary>
       /// The identifier of event
       /// </summary>
       [PrimaryKey]
       public Guid EventKey { get; set; }

       public string Title { get; set; }

       public DateTime EventStartDate { get; set; }

       public DateTime EventEndDate { get; set; }
   }
    
}
