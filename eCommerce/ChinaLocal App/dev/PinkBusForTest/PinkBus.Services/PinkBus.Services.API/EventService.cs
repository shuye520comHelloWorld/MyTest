using PinkBus.Services.Contract;
using PinkBus.Services.Interface;
using PinkBus.Services.OAuth.Attribute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PinkBus.Services.API
{

   [OAuthRequest(ProjectType.Intouch)]
    public class EventService:ServiceStack.Service
    {
        private IEventRepository eventRepository;

        public EventService(IEventRepository eventRepository) 
        {
            this.eventRepository = eventRepository;
        }

        public QueryEventResponse Get(QueryEvent dto)        
        {
           return eventRepository.QueryEvent(dto);
        }

      
    }
}
