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
using ServiceStack;
using System.Net;
using System.Data;
using PinkBus.Services.Infrastructure.SMS;
using PinkBus.Services.Common.Exception;
namespace PinkBus.Services.Infrastructure
{
    public class InvitationRepository : BaseRepository, IInvitationRepository
    {
        private const string SP_GetInvitation = "dbo.PB_GetInvitationInfo";
        private const string SP_PhoneNumber_IsBCValidation = "dbo.Usp_GetConsultantInfoByPhone";
        private const string SP_PhoneNumber_IsExistValidation = "dbo.PB_CheckDuplicatePhoneNumber";

        private TicketOperation ticketOperation;
        private CustomerOperation customerOperation;
        private ConsultantOperation consultantOperation;
        public InvitationRepository()
        {
            ticketOperation = new TicketOperation(GlobalAppSettings.Community);
            customerOperation = new CustomerOperation(GlobalAppSettings.Community);
            consultantOperation = new ConsultantOperation(GlobalAppSettings.Community);
        }

        public CheckEventInvitationStatusResponse CheckEventInvitationStatus(CheckEventInvitationStatus dto)
        {
            Event info = base.GetEventEntity(dto.EventKey);
            bool result = true;
            string message = string.Empty;
            if (info.InvitationStartDate != null)
            {
                if (info.InvitationStartDate >= DateTime.Now)
                {
                    DateTime temp = info.InvitationStartDate.Value;
                    result = false;
                    message = string.Format("请在{0}月{1}日{2}:{3}之后，再向顾客发起邀约！",
                        temp.Month,
                        temp.Day,
                        temp.Hour,
                        temp.Minute<10?string.Format("0{0}",temp.Minute):temp.Minute.ToString());

                }
            }
            return new CheckEventInvitationStatusResponse
            {
                Result = result,
                Message=message
            };
        }

        public QueryInvitationResponse QueryInvitation(QueryInvitation dto)
        {
            var ticket = ticketOperation.GetSingleData(p => p.TicketKey == dto.TicketKey);
            if (ticket == null)
                throw new NotFoundException(string.Format("Invitation not found, Invitation ticket key {0}", dto.TicketKey));


            Event eventInfo = base.GetEventEntity(ticket.EventKey);
            var consultant = consultantOperation.GetSingleData(p => p.ConsultantKey == ticket.MappingKey);

            EventBaseInfo info = eventInfo.ToEventBaseInfo();
            info.EventStartDate = ticket.SessionStartDate;
            info.EventEndDate = ticket.SessionEndDate;
            WechatUserType userType = WechatUserType.Other;
            switch (ticket.TicketStatus)
            {
                case (int)TicketStatus.Created:
                //throw new NotFoundException(string.Format("ticket status is not correct, Invitation ticket key {0}", dto.TicketKey));

                case (int)TicketStatus.Bestowed:
                case (int)TicketStatus.Inviting:

                    if (dto._UserId == consultant.ContactId)
                        userType = WechatUserType.Inviter;
                    return new QueryInvitationResponse
                    {
                        EventBaseInfo = info,
                        TicketKey = ticket.TicketKey,
                        TicketType = (TicketType)ticket.TicketType,
                        TicketStatus = (TicketStatus)ticket.TicketStatus,
                        IsInvitationEnd = DateTime.Now > eventInfo.InvitationEndDate,
                        ConsultantPhone = consultant.PhoneNumber,
                        FirstName = consultant.FirstName,
                        LastName = consultant.LastName,
                        WechatUserType = userType
                    };

                case (int)TicketStatus.Canceled:
                case (int)TicketStatus.Invited:
                case (int)TicketStatus.Checkin:
                case (int)TicketStatus.UnCheckin:

                    var customer = customerOperation.GetSingleData(p => p.CustomerKey == ticket.CustomerKey);

                    if (dto.UnionID == customer.UnionID)
                        userType = WechatUserType.Customer;
                    if (dto._UserId == consultant.ContactId)
                        userType = WechatUserType.Inviter;

                    return new QueryInvitationResponse
                      {
                          EventBaseInfo = info,
                          TicketKey = ticket.TicketKey,
                          TicketType = (TicketType)ticket.TicketType,
                          TicketStatus = (TicketStatus)ticket.TicketStatus,
                          IsInvitationEnd = DateTime.Now > eventInfo.InvitationEndDate,
                          ConsultantPhone = consultant.PhoneNumber,
                          FirstName = consultant.FirstName,
                          LastName = consultant.LastName,
                          CustomerName = customer.CustomerName,
                          HeadImgUrl = customer.HeadImgUrl,
                          WechatUserType = userType
                      };
            };
            return null;

        }
        /// <summary>
        /// open the invitation link with browser
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public QueryInvitationResponse QueryInvitationBrowser(QueryInvitationBrowser dto)
        {
            var ticket = ticketOperation.GetSingleData(p => p.TicketKey == dto.TicketKey);
            if (ticket == null)
                throw new NotFoundException(string.Format("Invitation not found, Invitation ticket key {0}", dto.TicketKey));


            Event eventInfo = base.GetEventEntity(ticket.EventKey);
            var consultant = consultantOperation.GetSingleData(p => p.ConsultantKey == ticket.MappingKey);
            EventBaseInfo info = eventInfo.ToEventBaseInfo();
            info.EventStartDate = ticket.SessionStartDate;
            info.EventEndDate = ticket.SessionEndDate;
            WechatUserType userType = WechatUserType.Other;
            switch (ticket.TicketStatus)
            {
                case (int)TicketStatus.Created:
                //    throw new NotFoundException(string.Format("ticket status is not correct, Invitation ticket key {0}", dto.TicketKey));
                //1. Bestowed-》 ticket created
                case (int)TicketStatus.Bestowed:
                case (int)TicketStatus.Inviting:
                    return new QueryInvitationResponse
                    {
                        EventBaseInfo = info,
                        TicketKey = ticket.TicketKey,
                        TicketType = (TicketType)ticket.TicketType,
                        TicketStatus = (TicketStatus)ticket.TicketStatus,
                        IsInvitationEnd = DateTime.Now > eventInfo.InvitationEndDate,
                        ConsultantPhone = consultant.PhoneNumber,
                        FirstName = consultant.FirstName,
                        LastName = consultant.LastName,
                        WechatUserType = userType
                    };
                case (int)TicketStatus.Canceled:
                case (int)TicketStatus.Invited:
                case (int)TicketStatus.Checkin:
                case (int)TicketStatus.UnCheckin:
                    var customer = customerOperation.GetSingleData(p => p.CustomerKey == ticket.CustomerKey);

                    return new QueryInvitationResponse
                    {
                        EventBaseInfo = info,
                        TicketKey = ticket.TicketKey,
                        TicketType = (TicketType)ticket.TicketType,
                        TicketStatus = (TicketStatus)ticket.TicketStatus,
                        IsInvitationEnd = DateTime.Now > eventInfo.InvitationEndDate,
                        ConsultantPhone = consultant.PhoneNumber,
                        FirstName = consultant.FirstName,
                        LastName = consultant.LastName,
                        WechatUserType = userType,
                        CustomerName = customer.CustomerName,
                        //HeadImgUrl = customer.HeadImgUrl,
                        //WechatUserType = userType
                    };
            }
            return null;
        }
        private bool CheckIsConsultantByPhoneNumber(string phoneNumber)
        {
            var parameters = new SqlParameter[]
                        {                         
                            new SqlParameter("@PhoneNumber",phoneNumber)                               
                        };
            var validationInfo = RepositoryHelper.Query<PhoneNumberValidationInfo>(GlobalAppSettings.ContactsLite, SP_PhoneNumber_IsBCValidation,
                parameters);
            
            if (validationInfo == null || validationInfo.Count == 0)
                return false;

            if (!string.IsNullOrEmpty(validationInfo.FirstNonDefault().ConsultantLevelID) && int.Parse(validationInfo.FirstNonDefault().ConsultantLevelID) < 15)            
                return false;
            
            return true;
        }

        private bool DuplicatePhoneNumberCheck(string phoneNumber, Guid eventKey)
        {
            var parameters = new SqlParameter[]
                        {                         
                            new SqlParameter("@PhoneNumber",phoneNumber),
                            new SqlParameter("@EventKey",eventKey.ToString())    
                        };
            //parameters[0].Value = phoneNumber;
            //parameters[1].Value = eventKey;     
            //int result = SqlHelper.ExecuteNonQuery(GlobalAppSettings.Community,CommandType.StoredProcedure, SP_PhoneNumber_IsExistValidation,
            //      parameters);

            var result = RepositoryHelper.ExecuteScalar(GlobalAppSettings.Community, SP_PhoneNumber_IsExistValidation,
                parameters);

            if (result != null)
                return true;
            else
                return false;
        }
        public AcceptInvitationResponse AcceptInvitation(AcceptInvitation dto)
        {
            Entity.Ticket ticket = ticketOperation.GetDataByFunc(p => p.TicketKey == dto.TicketKey).FirstOrDefault();
            //check event invitation end date
            Event eventInfo = base.GetEventEntity(ticket.EventKey);
            if (eventInfo.InvitationEndDate < DateTime.Now)
            {
                throw new NotFoundWithNoEmailException("领取时间已截止，请联系您的美容顾客");
            }
            #region Validation
            if (ticket == null)
                throw new NotFoundException(string.Format("ticket not found , ticket key {0}", dto.TicketKey));

            if (ticket.TicketStatus != (int)TicketStatus.Inviting && ticket.TicketStatus != (int)TicketStatus.Created)
                throw new NotFoundWithNoEmailException("该邀请券已被使用，请联系您的美容顾问");

            if (DuplicatePhoneNumberCheck(dto.CustomerPhone, ticket.EventKey))
                //throw new NotFoundException(string.Format("consultant profile not found, event key {0}", dto.EventKey));
                throw new HttpError(HttpStatusCode.BadRequest,
                                        "DuplicatePhoneNumber",
                                         GlobalAppSettings.DuplicatePhoneNumer_ErrorMessage);


            if (CheckIsConsultantByPhoneNumber(dto.CustomerPhone))
                throw new HttpError(HttpStatusCode.BadRequest,
                                       "ConsulantPhoneNumber",
                                        GlobalAppSettings.ConsultantLevel_ErrorMessage);
            #endregion


            Entity.Customer customer = new Entity.Customer();

            //if (dto.Career == null)
            //{
            customer = new Entity.Customer
            {
                AgeRange = (int)dto.AgeRange,
                InterestInCompany = dto.InterestInCompany,
                InterestingTopic = dto.InterestingTopic,
                UsedProduct = dto.UsedProduct,
                UsedSet = dto.UsedSet,
                CustomerName = dto.CustomerName,
                CustomerType = (int)dto.CustomerType,
                CustomerPhone = dto.CustomerPhone,
                UnionID = dto.UnionID,
                HeadImgUrl = dto.HeadImgUrl,
                BeautyClass = dto.BeautyClass,
                Source = (int)dto.Source,
                Career = dto.Career.HasValue ? (int)dto.Career.Value : new Nullable<int>(),
                //AcceptLevel = level,
                //CustomerContactId = CustomerContactId,
                CreatedDate = DateTime.Now,
                CustomerKey = Guid.NewGuid()
            };
            //}
            //else
            //{
            //    customer = new Entity.Customer
            //    {
            //        AgeRange = (int)dto.AgeRange,
            //        InterestInCompany = dto.InterestInCompany,
            //        InterestingTopic = dto.InterestingTopic,
            //        UsedProduct = dto.UsedProduct,
            //        UsedSet = dto.UsedSet,
            //        CustomerName = dto.CustomerName,
            //        CustomerType = (int)dto.CustomerType,
            //        CustomerPhone = dto.CustomerPhone,
            //        UnionID = dto.UnionID,
            //        HeadImgUrl = dto.HeadImgUrl,
            //        BeautyClass = dto.BeautyClass,
            //        Source = (int)dto.Source,
            //        Career = (int)dto.Career,
            //        //AcceptLevel = level,
            //        //CustomerContactId = CustomerContactId,
            //        CreatedDate = DateTime.Now,
            //        CustomerKey = Guid.NewGuid()
            //    };
            //}

            ticket = ticketOperation.GetDataByFunc(p => p.TicketKey == dto.TicketKey).FirstOrDefault();

            customerOperation.SaveWithTransaction((m) =>
            {
                m.Db.Insert(customer);
                ticket.CustomerKey = customer.CustomerKey;
                ticket.TicketStatus = (int)TicketStatus.Invited;
                m.Db.Update(ticket);
            });

          
            #region send sms
            if (dto.Source == Source.IntouchClient)
            {
                try
                {
                    Consultant consultant = consultantOperation.GetSingleData(p => p.ConsultantKey == ticket.MappingKey);

                    string ticketType = ticket.TicketType == (int)TicketType.Normal ? "来宾票" : "贵宾票";
                    string startDate = string.Empty;
                    string endDate = string.Empty;
                    if (ticket.SessionStartDate.Day == ticket.SessionEndDate.Day)
                    {
                        startDate = ticket.SessionStartDate.ToString("yyyy-MM-dd HH:mm");
                        endDate = string.Format("{0}:{1}", ticket.SessionEndDate.Hour, 
                            ticket.SessionEndDate.Minute<10?string.Format("0{0}",ticket.SessionEndDate.Minute):ticket.SessionEndDate.Minute.ToString());
                    }
                    else
                    {
                        startDate = ticket.SessionStartDate.ToString("yyyy-MM-dd HH:mm");
                        endDate = ticket.SessionEndDate.ToString("yyyy-MM-dd HH:mm");
                    }

                    //send sms

                    string smsContent = string.Format(GlobalAppSettings.InvitatedCustomer_Intouch_SMSTemplate,
                        customer.CustomerName,
                        consultant.LastName + consultant.FirstName,
                        ticketType,
                        string.Format("{0}-{1}", startDate, endDate),
                        eventInfo.EventLocation
                        );
                    bool sendSms = false;
                    int count = 0;
                    while (!sendSms && count <= 2)
                    {
                        sendSms = SMSHelper.SendSMS(customer.CustomerPhone, smsContent, string.Empty);
                        count++;
                    }
                    LogHelper.Info(string.Format("Send sms as inivated new customer {0} ,{1}", customer.CustomerName, customer.CustomerPhone));
                }
                catch (Exception ex)
                {
                    LogHelper.Error(string.Format("Send Sms notification got exception while invited one new customer in intouch {0},{1}", ticket.TicketKey, ex.Message + ex.StackTrace));
                }

            }
            #endregion


            AcceptInvitationResponse response = new AcceptInvitationResponse
            {
                CustomerKey = customer.CustomerKey,
                CustomerName = customer.CustomerName,
                CustomerPhone = customer.CustomerPhone,
                EventBaseInfo = eventInfo.ToEventBaseInfo(),
                Level = customer.AcceptLevel,
                ContactId = customer.CustomerContactId,
                TicketKey = ticket.TicketKey,
                TicketType = (TicketType)ticket.TicketType,
                Result = true

            };
            return response;
        }

    }
}
