using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PinkBus.Services.Interface;
using PinkBus.Services.Contract;
using PinkBus.Services.Entity.Operation;
using PinkBus.Services.Common;
using System.Data.SqlClient;
using PinkBus.Services.Infrastructure.Common;
using Entity = PinkBus.Services.Entity;
using PinkBus.Services.Infrastructure.Mapper;
using PinkBus.Services.Entity;
using contract=PinkBus.Services.Contract;
namespace PinkBus.Services.Infrastructure
{
    public class EventRepository : BaseRepository, IEventRepository
    {
        private const string SP_GetEventListByContactId = "PB_Event_GetList";
        private const string SP_GetTicketListByContactId = "PB_Ticket_GetList";
        //private EventOperation eventOperation;
        private ConsultantOperation consultantOperation;
        private EventSessionOperation eventSessionOperation;
        private TicketOperation ticketOperation;

        public EventRepository()
            : base()
        {
            //eventOperation = new EventOperation(GlobalAppSettings.Community);
            consultantOperation = new ConsultantOperation(GlobalAppSettings.Community);
            eventSessionOperation = new EventSessionOperation(GlobalAppSettings.Community);
            ticketOperation = new TicketOperation(GlobalAppSettings.Community);
        }

        #region private method

        #region Construct Response Information

        private EventBaseInfo ConstructBCResponse(EventConsultantInfo eventConsultant, string responseType)
        {
            EventBaseInfo response = null;
            switch (responseType)
            {
                case "EventQueryForBCResponse":
                    response = new EventQueryForBCResponse
                 {
                     BCType = (EventUserType)eventConsultant.UserType,
                     MappingKey = eventConsultant.MappingKey,
                     EventKey = eventConsultant.EventKey,
                     EventTitle = eventConsultant.EventTitle,
                     Location = eventConsultant.EventLocation,
                     EventStartDate = eventConsultant.CheckInStartDate,
                     EventEndDate = eventConsultant.CheckInEndDate
                 };
                    break;
                case "EventQueryForVolunteerResponse":
                    response = new EventQueryForVolunteerResponse
                 {
                     BCType = (EventUserType)eventConsultant.UserType,
                     MappingKey = eventConsultant.MappingKey,
                     EventKey = eventConsultant.EventKey,
                     EventTitle = eventConsultant.EventTitle,
                     Location = eventConsultant.EventLocation,
                     EventStartDate = eventConsultant.CheckInStartDate,
                     EventEndDate = eventConsultant.CheckInEndDate
                 };
                    break;
                case "EventQueryForVipResponse":
                    response = new EventQueryForVipResponse
                    {
                        BCType = (EventUserType)eventConsultant.UserType,
                        MappingKey = eventConsultant.MappingKey,
                        EventKey = eventConsultant.EventKey,
                        EventTitle = eventConsultant.EventTitle,
                        Location = eventConsultant.EventLocation,
                        EventStartDate = eventConsultant.CheckInStartDate,
                        EventEndDate = eventConsultant.CheckInEndDate
                    };
                    break;
            }

            return response;
        }

        private EventStage DetectSharedEventStage(EventConsultantInfo eventConsultant, DateTime? currentDateTime)
        {
            if (currentDateTime == null || !currentDateTime.HasValue)
                currentDateTime = DateTime.Now;

            if (currentDateTime >= eventConsultant.ApplyTicketEndDate && currentDateTime < eventConsultant.InvitationEndDate)
                return EventStage.OpenForInvitation;
            //TODO : eventConsultant.EventCheckInStartDate=min session (by dispaly order)'s startdate
            else if (currentDateTime >= eventConsultant.InvitationEndDate && currentDateTime < eventConsultant.CheckInStartDate)
                return EventStage.OpenForDownloadData;
            else if (currentDateTime >= eventConsultant.CheckInStartDate && currentDateTime < eventConsultant.EventEndDate.Date.AddDays(1))
                return EventStage.OpenForOffline;
            else // currentDateTime >= eventConsultant.EventEndDate)
                return EventStage.OpenForHistory;

        }

        #endregion

        #region Calculate ticket count by event stage

        private EventQueryBaseResponse CalulateTicketCount(EventQueryBaseResponse baseResponse)
        {
            //List<Entity.Ticket> ticketList = ticketOperation.GetAllData().FindAll(p=>p.TicketType==1);
            ///List<Entity.Ticket> ticketList = ticketOperation.GetDataByFunc(p => p.ConsultantKey == baseResponse.MappingKey);
            var Parameters = new SqlParameter[]
                        {                         
                            new SqlParameter("@MappingKey",baseResponse.MappingKey),                              
                        };
            var ticketList = RepositoryHelper.Query<TicketInfo>(GlobalAppSettings.Community, SP_GetTicketListByContactId,
                Parameters)
                .ToList();

            baseResponse.InvitedCustomerCount = ticketList.Where(p => p.TicketStatus == (int)TicketStatus.Invited).Count();


            baseResponse.CheckedInCustomerCount = ticketList.Where(p => p.TicketStatus == (int)TicketStatus.Checkin).Count();

            baseResponse.NormalTicketLeftCount = ticketList.Where(p => (p.TicketStatus == (int)TicketStatus.Created
                             || p.TicketStatus == (short)TicketStatus.Inviting)
                             && p.TicketType == (short)TicketType.Normal).Count();

            baseResponse.VIPTicketLeftCount = ticketList.Where(p => (p.TicketStatus == (int)TicketStatus.Created
                             || p.TicketStatus == (short)TicketStatus.Inviting)
                             && p.TicketType == (short)TicketType.VIP).Count();

            return baseResponse;
        }

        private bool CheckIfTicketOutForAllCanApplySession(Guid eventKey)
        {
            var result = eventSessionOperation.GetDataByFunc(p => p.Eventkey == eventKey && p.CanApply==true).ToList();
            bool ticketOutForAllCanApplySession = true;
            result.ForEach((item) =>
            {
                if (item.TicketOut == false)
                    ticketOutForAllCanApplySession = false;
            });

            return ticketOutForAllCanApplySession;
        }

        #endregion

        //private bool CheckIfTicketOutForAllCanApplySession(Guid eventKey)
        //{
        //    var result = eventSessionOperation.GetDataByFunc(p => p.Eventkey == eventKey).ToList();
        //    bool ticketOutForAllCanApplySession = true;
        //    result.ForEach((item) =>
        //    {
        //        if (item.TicketOut == false)
        //            ticketOutForAllCanApplySession = false;
        //    });

        //    return ticketOutForAllCanApplySession;
        //}

        /// <summary>
        /// construct EventQueryForBCResponse
        /// </summary>
        /// <param name="eventConsultant"></param>
        /// <returns></returns>
        private EventQueryForBCResponse GetEventForBCResponse(EventConsultantInfo eventConsultant)
        {
            EventQueryForBCResponse response = (EventQueryForBCResponse)ConstructBCResponse(eventConsultant, "EventQueryForBCResponse");
            DateTime currentDateTime = DateTime.Now;
            if (currentDateTime < eventConsultant.ApplyTicketStartDate)
            {
                response.EventStage = EventStage.OpenForCountDown;
                response.TimeRemaining = (int)(eventConsultant.ApplyTicketStartDate - currentDateTime).TotalSeconds;
            }
            else if (currentDateTime >= eventConsultant.ApplyTicketStartDate && currentDateTime < eventConsultant.ApplyTicketEndDate)
            {
                if (eventConsultant.UserType == (short)EventUserType.BestowalBC)
                {
                    response.EventStage = EventStage.OpenForInvitation;
                }
                else
                {
                    if (CheckIfTicketOutForAllCanApplySession(eventConsultant.EventKey))
                    {
                        //status define refer CosnultantStatus
                        if (eventConsultant.VIPTicketQuantity == 0 && eventConsultant.NormalTicketQuantity == 0 && eventConsultant.Status == 0)
                            response.EventStage = EventStage.OpenForTicketOut;
                        else
                            response.EventStage = EventStage.OpenForInvitation;
                    }
                    else
                    {
                        //status define refer CosnultantStatus
                        if (eventConsultant.Status == 1)
                            response.EventStage = EventStage.OpenForInvitation;
                        else
                            response.EventStage = EventStage.OpenForScrambleTicket;
                    }
                }
            }
            else
            {
                response.EventStage = DetectSharedEventStage(eventConsultant, currentDateTime);
                if (response.EventStage == EventStage.OpenForInvitation)
                {
                    if (eventConsultant.Status == 0 && eventConsultant.NormalTicketQuantity == 0 &&eventConsultant.VIPTicketQuantity==0)
                        response.EventStage = EventStage.OpenForApplyTicketEnd;
                }
            }

            switch (response.EventStage)
            {
                case EventStage.OpenForCountDown:
                case EventStage.OpenForScrambleTicket:
                    response.NormalTicketLeftCount = eventConsultant.NormalTicketQuantity;
                    response.VIPTicketLeftCount = eventConsultant.VIPTicketQuantity;
                    response.InvitedCustomerCount = 0;
                    break;
                case EventStage.OpenForInvitation:
                case EventStage.OpenForDownloadData:
                case EventStage.OpenForOffline:
                case EventStage.OpenForHistory:
                    response = (EventQueryForBCResponse)CalulateTicketCount(response);
                    break;
            }

            return response;
        }

        private EventQueryForVolunteerResponse GetEventForVolunteerBCResponse(EventConsultantInfo eventConsultant)
        {
            //bool CountyConfirmed = false;
            //if (!string.IsNullOrEmpty(eventConsultant.CountyName))
            //    CountyConfirmed = true;
            EventQueryForVolunteerResponse response = (EventQueryForVolunteerResponse)ConstructBCResponse(eventConsultant, "EventQueryForVolunteerResponse");
            response.CountyConfirmed = eventConsultant.IsConfirmed;

            response.EventStage = DetectSharedEventStage(eventConsultant, null);
            response = (EventQueryForVolunteerResponse)CalulateTicketCount(response);
            return response;
        }

        private EventQueryForVipResponse GetEventForVipResponse(EventConsultantInfo eventConsultant)
        {
            EventQueryForVipResponse response = (EventQueryForVipResponse)ConstructBCResponse(eventConsultant, "EventQueryForVipResponse");

            DateTime currentDateTime = DateTime.Now;
            response.EventStage = DetectSharedEventStage(eventConsultant, null);
            response = (EventQueryForVipResponse)CalulateTicketCount(response);
            return response;
        }


        #endregion

        public QueryEventResponse QueryEvent(QueryEvent dto)
        {
            QueryEventResponse response = new QueryEventResponse { };
            //get event list
            if (dto.EventKey == Guid.Empty)
            {
                var Parameters = new SqlParameter[]
                        {                         
                            new SqlParameter("@ContactID",dto._UserId.ToString()),                         
                        };
                var result = RepositoryHelper.Query<EventConsultantInfo>(GlobalAppSettings.Community, SP_GetEventListByContactId,
                    Parameters);

                List<EventQueryForBCResponse> bcResponseList = new List<EventQueryForBCResponse>();
                List<EventQueryForVolunteerResponse> volunteerResponseList = new List<EventQueryForVolunteerResponse>();
                List<EventQueryForVipResponse> vipResponseList = new List<EventQueryForVipResponse>();

                foreach (var eventConsultantInfo in result)
                {
                    EventUserType userType = (EventUserType)eventConsultantInfo.UserType;
                    switch (userType)
                    {
                        case EventUserType.NormalBC:
                            bcResponseList.Add(GetEventForBCResponse(eventConsultantInfo));
                            break;
                        case EventUserType.VolunteerBC:
                            volunteerResponseList.Add(GetEventForVolunteerBCResponse(eventConsultantInfo));
                            break;
                        case EventUserType.VIPBC:
                            vipResponseList.Add(GetEventForVipResponse(eventConsultantInfo));
                            break;
                        case EventUserType.BestowalBC:
                            bcResponseList.Add(GetEventForBCResponse(eventConsultantInfo));
                            break;
                    }
                }

                response.NormalBC = bcResponseList;
                response.Volunteer = volunteerResponseList;
                response.Vip = vipResponseList;
                return response;
            }
            else
            {
                //Get event session
                EventBaseInfo baseInfo = base.GetEventBaseInfo(dto.EventKey);
                int applyQuantityPerSession = base.GetEventTicketSetting(dto.EventKey).TicketQuantityPerSession;
                List<contract.EventSession> sessionList = eventSessionOperation.GetDataByFunc(p => p.Eventkey == dto.EventKey && p.CanApply == true)
                                                .ToList()
                                                .EventSessionToResponse(applyQuantityPerSession);
                EventSessionResponse sessionResponse = new EventSessionResponse
                {
                    EventSession = sessionList,
                    EventKey = baseInfo.EventKey,
                    EventTitle = baseInfo.EventTitle,
                    Location = baseInfo.Location,
                    EventStartDate = baseInfo.EventStartDate,
                    EventEndDate = baseInfo.EventEndDate
                };
                return new QueryEventResponse { EventSession = sessionResponse };
            }

        }
    }
}
