using iParty.Services.Entity;
using QuartES.Services.Core;
using System;
using System.Collections.Generic;

namespace iParty.Services.Interface
{
    public interface IWorkshopsProvider
    {
        IEnumerable<Workshop> GetWorkshops(Guid eventKey);
        Workshop GetWorkshopById(int WorkshopId, Guid eventKey);
      
    }
}
