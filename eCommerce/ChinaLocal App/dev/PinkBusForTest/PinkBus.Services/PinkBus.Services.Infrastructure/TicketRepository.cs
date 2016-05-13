using PinkBus.Services.Common;
using PinkBus.Services.Entity.Operation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PinkBus.Services.Contract;
using PinkBus.Services.Interface;
using PinkBus.Services.Entity;
using PinkBus.Services.Infrastructure.Mapper;
using System.Data.SqlClient;
using PinkBus.Services.Infrastructure.Common;
using contract = PinkBus.Services.Contract;
using ServiceStack.OrmLite;
using System.Data;
using PinkBus.Services.Infrastructure.SMS;
using PinkBus.Services.Common.Exception;
namespace PinkBus.Services.Infrastructure
{
    public class TicketRepository : BaseRepository, ITicketRepository
    {
        private const string SP_GetTicketListByContactId = "PB_Ticket_GetList";
        private const string SP_PB_Ticket_GetDetail = "PB_Ticket_GetDetail";
        private const string SP_PB_ConsultantInfo = "PB_ConsultantInfo";

        private const string ConsultantNotFound = "转赠失败，编号不存在";
        private const string PhoneNumberNotFound = "转赠失败,对方手机号码不存在";
         

        private TicketOperation ticketOperation;
        private ApplyTicketTrackerOperation ticketTrackerOperation;
        private ConsultantOperation consultantOperation;
        private CustomerOperation customerOperation;
        public TicketRepository()
            : base()
        {
            ticketOperation = new TicketOperation(GlobalAppSettings.Community);
            ticketTrackerOperation = new ApplyTicketTrackerOperation(GlobalAppSettings.Community);
            consultantOperation = new ConsultantOperation(GlobalAppSettings.Community);
            customerOperation = new CustomerOperation(GlobalAppSettings.Community);
        }

        public QueryTicketsResponse QueryTickets(QueryTickets dto)
        {
            EventBaseInfo baseInfo = base.GetEventBaseInfo(dto.EventKey);
            QueryTicketsResponse response = new QueryTicketsResponse { EventBaseInfo = baseInfo };

            var Parameters = new SqlParameter[]
                        {                         
                            new SqlParameter("@ContactID",dto._UserId.ToString()),  
                            new SqlParameter("@EventKey",dto.EventKey),    
                        };
            var tickets = RepositoryHelper.Query<TicketInfo>(GlobalAppSettings.Community, SP_GetTicketListByContactId,
                Parameters)
                .ToList()
                .TicketToResponse();

            response.Tickets = tickets;

            return response;
        }

        public GetTicketDetailResponse GetTicketDetail(GetTicketDetail dto)
        {
            Event eventInfo = base.GetEventEntity(dto.EventKey);

            var Parameters = new SqlParameter[]
                        {                         
                            new SqlParameter("@TicketKey",dto.TicketKey)                               
                        };
            var ticketDetail = RepositoryHelper.Query<TicketDetailInfo>(GlobalAppSettings.Community, SP_PB_Ticket_GetDetail,
                Parameters).Single();
            if (ticketDetail == null)
                throw new NotFoundException(string.Format("ticket not found , ticket key {0}", dto.TicketKey));

            GetTicketDetailResponse response = ticketDetail.TicketDetailInfoToResponse();
            response.EventBaseInfo = eventInfo.ToEventBaseInfo();
            response.InvitationEndDate = eventInfo.InvitationEndDate;
            response.SharedUrl = GlobalAppSettings.SharedUrl + dto.TicketKey;
            return response;
        }

        public ApplyTicketResponse ApplyTicket(ApplyTicket dto)
        {

            //TODO Get consultant key
            ApplyTicketTracker tracker = new ApplyTicketTracker
            {
                SessionKey = dto.SessionKey,
                ApplyTicketTrackerId = Guid.NewGuid(),
                MappingKey = dto.MappingKey,
                ApplyTicketResult = (int)ApplyTicketResult.Unknown,
                Status = 0,
                CreatedBy = dto._UserId.ToString(),
                CreatedDate = DateTime.Now
            };

            ticketTrackerOperation.Create(tracker);

            return new ApplyTicketResponse
            {
                ApplyTicketTrackerId = tracker.ApplyTicketTrackerId
            };
        }

        public QueryApplyTicketResultResponse QueryApplyTicketResult(QueryApplyTicketResult dto)
        {
            var ticketResult = ticketTrackerOperation.GetSingleData(p => p.ApplyTicketTrackerId == dto.ApplyTicketTrackerId);
            if (ticketResult == null)
                throw new NotFoundException(string.Format("ApplyTicketResult not found , ApplyTicketResult key {0}", dto.ApplyTicketTrackerId));
            if (ticketResult != null)
                return new QueryApplyTicketResultResponse
                {
                    ApplyTicketResult = (ApplyTicketResult)ticketResult.ApplyTicketResult
                };

            return null;
        }

        private ConsultantInfo GetConsultantInfo(string directSellerId)
        {
            StringBuilder parameterStr = new StringBuilder();
            parameterStr.Append("<root> <s ");
            parameterStr.Append(string.Format("id='{0}'", directSellerId));
            parameterStr.Append(" /> </root>");

            var parameters = new SqlParameter[]
                        {                         
                            new SqlParameter("@IdsXml",parameterStr.ToString())                               
                        };

            var consultantInfo = RepositoryHelper.Query<ConsultantInfo>(GlobalAppSettings.Community, SP_PB_ConsultantInfo,
              parameters);

            if (consultantInfo == null || consultantInfo.Count == 0)
                throw new NotFoundWithNoEmailException(ConsultantNotFound);

            return consultantInfo.FirstOrDefault();
        }

        public BestowalTicketResponse BestowalTicket(BestowalTicket dto)
        {
            Entity.Ticket ticket = ticketOperation.GetSingleData(p => p.TicketKey == dto.TicketKey);

            if (ticket == null)
                throw new NotFoundException(string.Format("ticket not found , ticket key {0}", dto.TicketKey));

            Event eventInfo = base.GetEventEntity(ticket.EventKey);
            if (eventInfo.InvitationEndDate < DateTime.Now)
            {
                throw new NotFoundWithNoEmailException("在线邀约已截止，无法进行转增");
            }
            if (ticket.TicketStatus != (int)TicketStatus.Created)
                throw new NotFoundException(string.Format("ticket status incorrect , ticket key {0}", dto.TicketKey));

            var consultantInfo = GetConsultantInfo(dto.DirectSellerId);
            if (consultantInfo == null || (consultantInfo.LastName + consultantInfo.FirstName != dto.ConsultantName))
                throw new NotFoundWithNoEmailException(GlobalAppSettings.DirectSellerIdFailed_BestowalTicket_ErrorMessage);

            if (int.Parse(consultantInfo.Level) < 35)
                throw new NotFoundWithNoEmailException(GlobalAppSettings.ConsnultantLevelFailed_BestowalTicket_ErrorMessage);

             if (string.IsNullOrEmpty(consultantInfo.PhoneNumber))
                 throw new NotFoundWithNoEmailException(PhoneNumberNotFound);

            
            Entity.Consultant bestowalConsultant = consultantOperation.GetSingleData(p => p.EventKey == dto.EventKey
                                                                                     && p.ContactId == consultantInfo.ContactID);
            Entity.Ticket newTicket = null;

            ticketOperation.SaveWithTransaction((m) =>
            {
                ticket.TicketStatus = (int)TicketStatus.Bestowed;
                ticket.UpdatedBy = dto._UserId.ToString();
                ticket.UpdatedDate = DateTime.Now;
                m.Db.Update(ticket);

                if (bestowalConsultant == null)
                {
                    bestowalConsultant = new Consultant
                    {
                        ContactId = consultantInfo.ContactID,
                        EventKey = ticket.EventKey,
                        EventUserType = (int)EventUserType.BestowalBC,
                        PhoneNumber = consultantInfo.PhoneNumber,
                        FirstName = consultantInfo.FirstName,
                        LastName = consultantInfo.LastName,
                        DirectSellerId = dto.DirectSellerId,
                        Level = consultantInfo.Level,
                        City = consultantInfo.City,
                        Province = consultantInfo.Province,
                        ResidenceID = consultantInfo.ResidenceID,
                        ConsultantKey = Guid.NewGuid(),
                        NormalTicketQuantity = (TicketType)ticket.TicketType == TicketType.Normal ? 1 : 0,
                        VIPTicketQuantity = (TicketType)ticket.TicketType == TicketType.VIP ? 1 : 0,
                        CreatedDate = DateTime.Now,
                        CreatedBy = dto._UserId.ToString()
                    };
                    m.Db.Insert(bestowalConsultant);
                }
                else
                {
                    if ((TicketType)ticket.TicketType == TicketType.Normal)
                        bestowalConsultant.NormalTicketQuantity += 1;
                    else
                        bestowalConsultant.VIPTicketQuantity += 1;

                    bestowalConsultant.UpdatedDate = DateTime.Now;
                    m.Db.Update(bestowalConsultant);
                }

                newTicket = new Entity.Ticket
                {
                    EventKey = ticket.EventKey,
                    TicketStatus = (int)TicketStatus.Created,
                    TicketType = (int)ticket.TicketType,
                    TicketFrom = (int)TicketFrom.Bestowal,
                    MappingKey = bestowalConsultant.ConsultantKey,
                    SessionKey = ticket.SessionKey,
                    SessionStartDate = ticket.SessionStartDate,
                    SessionEndDate = ticket.SessionEndDate,
                    SMSToken = ticket.SMSToken,
                    TicketKey = Guid.NewGuid(),
                    CreatedDate = DateTime.Now,
                    CreatedBy = dto._UserId.ToString()

                };
                m.Db.Insert(newTicket);

            });

            return new BestowalTicketResponse
            {
                Result = true,
                TicketKey = newTicket.TicketKey,
                TicketStatus = (TicketStatus)newTicket.TicketStatus
            };
        }

        public CreateInvitationResponse CreateInvitation(CreateInvitation dto)
        {
            var ticket = ticketOperation.GetSingleData(p => p.TicketKey == dto.TicketKey);
            if (ticket == null)
                throw new NotFoundException(string.Format("ticket not found , ticket key {0}", dto.TicketKey));

            Event eventInfo = base.GetEventEntity(ticket.EventKey);
            if (eventInfo.InvitationEndDate < DateTime.Now)
            {
                throw new NotFoundWithNoEmailException("在线邀约已截止，可在活动现场继续使用");
            }

            if (ticket.TicketStatus != (int)TicketStatus.Inviting)
            {
                ticket.TicketStatus = (int)TicketStatus.Inviting;
                ticketOperation.Update(ticket);
            }
            return new CreateInvitationResponse { Result = true };

        }

        /// <summary>
        /// Cancel
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public CancelInvitationResponse CancelInvitation(CancelInvitation dto)
        {
            var ticket = ticketOperation.GetSingleData(p => p.TicketKey == dto.TicketKey);
            if (ticket == null)
                throw new NotFoundException(string.Format("ticket not found , ticket key {0}", dto.TicketKey));

            Event eventInfo = base.GetEventEntity(ticket.EventKey);
            if (eventInfo.InvitationEndDate < DateTime.Now)
            {
                throw new NotFoundWithNoEmailException("在线邀约已截止，可在活动现场继续使用");
            }
            Entity.Ticket newTicket = new Entity.Ticket
                          {
                              TicketKey = Guid.NewGuid(),
                              TicketFrom = (int)TicketFrom.Rebuild,
                              TicketStatus = (int)TicketStatus.Created,
                              MappingKey = ticket.MappingKey,
                              SessionStartDate = ticket.SessionStartDate,
                              SessionEndDate = ticket.SessionEndDate,
                              SMSToken = ticket.SMSToken,
                              SessionKey = ticket.SessionKey,
                              EventKey = ticket.EventKey,
                              TicketType = ticket.TicketType,
                              CreatedBy = dto._UserId.ToString(),
                              CreatedDate = DateTime.Now
                          };
            ticketOperation.SaveWithTransaction((m) =>
            {
                ticket.TicketStatus = (int)TicketStatus.Canceled;
                m.Db.Update(ticket);
                m.Db.Insert(newTicket);
            });

            try
            {
                //TODO : send sms
                //Event eventInfo = base.GetEventEntity(ticket.EventKey);
                var Parameters = new SqlParameter[]
                        {                         
                            new SqlParameter("@TicketKey",dto.TicketKey)                               
                        };
                var ticketDetail = RepositoryHelper.Query<TicketDetailInfo>(GlobalAppSettings.Community, SP_PB_Ticket_GetDetail,
                    Parameters).Single();

                string smsContent = string.Format(GlobalAppSettings.CancelTicket_Intouch_SMSTemplate,
                     ticketDetail.CustomerName,
                     eventInfo.EventTitle,
                     ticketDetail.LastName + ticketDetail.FirstName
                      );

                bool sendSms = false;
                int count = 0;
                while (!sendSms && count <= 2)
                {
                    sendSms = SMSHelper.SendSMS(ticketDetail.CustomerPhone, smsContent, string.Empty);
                    count++;
                }
                LogHelper.Info(string.Format("Send sms as ticket cancelled , customer {0} ,{1}", ticketDetail.CustomerName, ticketDetail.CustomerPhone));
            }
            catch (Exception ex)
            {
                LogHelper.Error(string.Format("Send Sms notification got exception while cancel one ticket {0},{1}", ticket.TicketKey, ex.Message + ex.StackTrace));
            }

            return new CancelInvitationResponse
            {

                Result = true
            };

        }
    }
}
