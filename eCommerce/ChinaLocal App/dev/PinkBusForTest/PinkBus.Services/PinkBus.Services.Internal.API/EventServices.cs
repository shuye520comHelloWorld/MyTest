using PinkBus.Services.Internal.Contract;
using PinkBus.Services.Internal.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PinkBus.Services.Internal.API
{
    public class EventServices:  ServiceStack.Service
    {
        private IEventRepository eventRepository;

        public EventServices(IEventRepository eventRepository) 
        {
            this.eventRepository = eventRepository;
        }

        public GetEventResponse Get(GetEvent dto)
        {
            return eventRepository.GetEvent(dto);
        }

      
    }
}
