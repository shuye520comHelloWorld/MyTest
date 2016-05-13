using PinkBus.Services.Common;
using PinkBus.Services.Entity.Operation;
using PinkBus.Services.Internal.Contract;
using PinkBus.Services.Internal.Infrastructure.Common;
using PinkBus.Services.Internal.Interface;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PinkBus.Services.Internal.Infrastructure
{
    public class EventRepository : IEventRepository
    {
        private EventOperation eventOperation;
        private const string SP_GetEventListByContactId = "PB_Event_GetList";

        public EventRepository()
        {
            eventOperation = new EventOperation(GlobalAppSettings.Community);
        }

        public GetEventResponse GetEvent(GetEvent dto)
        {
            var Parameters = new SqlParameter[]
                        {                         
                            new SqlParameter("@ContactID",dto.ContactId.ToString()),                         
                        };
            var result = RepositoryHelper.Query<EventConsultantInfo>(GlobalAppSettings.Community, SP_GetEventListByContactId,
                Parameters)
               .Select((p) =>
                {
                    return new Event {
                       EventKey=  p.EventKey,
                       EventName= p.EventTitle,
                       EventStartDate = p.EventStartDate,
                       EventEndDate = p.EventEndDate                       
                    };
                }).ToList();

            return new GetEventResponse { Events = result.Where(p=>p.EventStartDate>=DateTime.Now).ToList() };
        }
    }
}
