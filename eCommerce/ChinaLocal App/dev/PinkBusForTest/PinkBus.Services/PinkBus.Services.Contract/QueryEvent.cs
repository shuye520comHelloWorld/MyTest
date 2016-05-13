
using ServiceStack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PinkBus.Services.Common;
namespace PinkBus.Services.Contract
{

    [Api(@"1. Query BC' related event list while enrty intouch app (No paramter),;
           2. Query historical event (IsFinished=true);
           3. Query event detail information (EventKey)")]
    [Route("/events/query", "GET, OPTIONS")]
    public class QueryEvent : QueryRequestDto, IReturn<QueryEventResponse>
    {
        //eventkey 和eventstage要一起传入或者都为空，validator的时候进行校验
        //detail 也是通过这个API查询
        [ApiMember(Name = "EventKey", ParameterType = "body", DataType = "guid", Description = "the id of the Event ", IsRequired = false)]
        public Guid EventKey { get; set; }

        //[ApiMember(Name = "EventStage", ParameterType = "body", DataType = "enum", Description = "the stage of event {OpenForCountDown/OpenForScrambleTicket/OpenForInvitation/OpenForDownloadData/OpenForOffline/OpenForHistory}", IsRequired = false)]
        //public EventStage? EventStage { get; set; }

        //[ApiMember(Name = "IsHistorical", ParameterType = "body", Description = "only include the finished event", DataType = "bool", IsRequired = false)]
        //public bool IsHistorical { get; set; }

    }
  
    /// <summary>
    /// Query event response please refer EventQueryResult
    /// </summary>
    public class QueryEventResponse : QueryResponseDto
    {
        [ApiMember(Name = "NormalBC", ParameterType = "body", DataType = "List", Description = "return event query response for normal bc", IsRequired = false)]
        public List<EventQueryForBCResponse> NormalBC { get; set; }

        [ApiMember(Name = "Volunteer", ParameterType = "body", DataType = "List", Description = "return event query response for volunteer", IsRequired = false)]
        public List<EventQueryForVolunteerResponse> Volunteer { get; set; }

        [ApiMember(Name = "Vip", ParameterType = "body", DataType = "List", Description = "return event query response for vip", IsRequired = false)]
        public List<EventQueryForVipResponse> Vip { get; set; }

        [ApiMember(Name = "EventSession", ParameterType = "body", DataType = "object", Description = "return event query response for event session", IsRequired = false)]
        public EventSessionResponse EventSession { get; set; }

    }

     
}
