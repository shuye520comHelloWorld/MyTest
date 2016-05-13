using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PinkBus.CheckInClient.Entitys
{
    public class CheckTokenResponse
    {
        public Event Event { get; set; }
        public List<EventSession> EventSessions { get; set; }
        public ResponseStatus ResponseStatus { get; set; }
    }

    public class DownloadDataResponse
    {
        public string data { get; set; }
        public ResponseStatus ResponseStatus { get; set; }
    }

    public class ConsultantResponse
    {
        List<Consultant> Consultants { get; set; }
    }

    public class ResponseStatus
    {
        public string ErrorCode { get; set; }
        public string Message { get; set; }

        public string[] Errors { get; set; }
    }

    //ResponseStatus\":{\"ErrorCode\":\"NotFound\",\"Message\":\"活动码有误！\",\"Errors\":[]}

    public class UploadDataResponse
    {
        public bool Result { get; set; }
        public string ContactId { get; set; } 
        public ResponseStatus ResponseStatus { get; set; }
    }
}
