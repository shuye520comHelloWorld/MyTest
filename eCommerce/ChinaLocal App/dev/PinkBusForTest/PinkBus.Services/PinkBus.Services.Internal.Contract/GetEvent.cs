using PinkBus.Services.Common;
using ServiceStack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PinkBus.Services.Internal.Contract
{


    [Api("get event list")]
    [Route("/events", "GET,Options")]
    public class GetEvent : BaseRequestDto, IReturn<GetEventResponse>
    {
        [ApiMember(Name = "ContactId", ParameterType = "body", Description = "the contact id of consultant", DataType = "long", IsRequired = true)]
        public long ContactId { get; set; }
    }

    public class GetEventResponse : BaseResponseDto
    {
        [ApiMember(Name = "PinkBusEvent", ParameterType = "body", Description = "event  list ", DataType = "list", IsRequired = true)]
        public List<Event> Events { get; set; }
    }

    public class Event
    {

        [ApiMember(Name = "EventKey", ParameterType = "body", Description = "the key of event, ", DataType = "guid", IsRequired = true)]
        public Guid EventKey { get; set; }

        [ApiMember(Name = "EventName", ParameterType = "body", Description = "the name of event, ", DataType = "string", IsRequired = true)]
        public string EventName { get; set; }

        [ApiMember(Name = "EventStartDate", ParameterType = "body", Description = "the start date of event  ", DataType = "datetime", IsRequired = true)]
        public DateTime EventStartDate { get; set; }

        [ApiMember(Name = "EventEndDate", ParameterType = "body", Description = "the end date of event ", DataType = "datetime", IsRequired = true)]
        public DateTime EventEndDate { get; set; }
    }

}
