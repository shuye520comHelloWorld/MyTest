using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PinkBus.EventManagement.Models
{
    public class PB_Event
    {
        public System.Guid EventKey { get; set; }
        public string EventLocation { get; set; }
        public string EventTitle { get; set; }
        public DateTime EventStartDate { get; set; }
        public DateTime EventEndDate { get; set; }
        public DateTime ApplyTicketStartDate { get; set; }
        public DateTime ApplyTicketEndDate { get; set; }
        public DateTime? InvitationStartDate { get; set; }
        public DateTime InvitationEndDate { get; set; }
        public DateTime CheckinStartDate { get; set; }
        public DateTime CheckinEndDate { get; set; }
        public string DownloadToken { get; set; }       
        public string UploadToken { get; set; }
        public bool? IsUpload { get; set; }
        public bool BCImport { get; set; }
        public bool VolunteerImport { get; set; }
        public bool VipImport { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string UpdatedBy { get; set; }

        public List<PB_EventSession> EventSessions { get; set; }
    }

    public class PB_EventSession
    {
        public Guid SessionKey { get; set; }
        public Guid EventKey { get; set; }
        public short DisplayOrder { get; set; }
        public DateTime SessionStartDate { get; set; }
        public DateTime  SessionEndDate { get; set; }
        public bool CanApply { get; set; }
        public int VIPTicketQuantity { get; set; }
        public int NormalTicketQuantity { get; set; }
        public DateTime  CreatedDate { get; set; }
        public string CreatedBy { get; set; }

        public int NormalTicketUsedCount { get; set; }
        public int VipTicketUsedCount { get; set; }
        public DateTime?  UpdatedDate { get; set; }
        public string UpdatedBy { get; set; }

    }


    public partial class PB_EventTicketSetting
    {
        public int Id { get; set; }
        public System.Guid EventKey { get; set; }
        public int TicketQuantityPerSession { get; set; }
        public int ApplyTicketTotal { get; set; }
        public int? VolunteerNormalTicketCountPerPerson { get; set; }
        public int? VolunteerVIPTicketCountPerPerson { get;set;}
        public System.DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public System.DateTime? UpdatedDate { get; set; }
        public string UpdatedBy { get; set; }

    }

   


}