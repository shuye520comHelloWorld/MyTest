using iParty.Services.Contract;
using iParty.Services.Entity;
using iParty.Services.Interface;
using iParty.Services.Interface.Exception;
using iParty.Services.ORM.Operations;
using iParty.Services.ORM;
using QuartES.Services.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using ServiceStack;
using System.Net;
using System.Security.AccessControl;
using ServiceStack.OrmLite;
using NLog;

namespace iParty.Services.Infrastructure
{
    public class InvitationRepository : IInvitationRepository
    {
        readonly Logger _logger = LogManager.GetCurrentClassLogger();
        private const string InvitationQRFormat = "{0}|{1}|{2}"; //PartyKey|CustomerKey|InvitaitonKey
        private IContext _context;
        private CustomerBCOperation _customerBCOperation;
        private CustomerOperation _customerOperation;
        private InvitationOperation _invitationOperation;
        private PartyOperation _partyOperation;
        private ConsultantOperation _consultantOperation;
        private EventOperation _eventOperation;
        private IPartyRepository _partyRepository;
        private IThreeDES _threeDes;
        private IEventsProvider _eventsProvider;
        private IWorkshopsProvider _workshopProvider;
        private UnitOperation _unitOperation;
        private CustomerDetailOperation _customerDetailOperation;
        private CustomerListOperation _customerListOperation;
        private WeChartCardCodeOperation _weCardCodeOperation;
        private UniteeOperation _uniteeOperation;
        private IHttpRequestHandler _httpRequestHandler;
        public InvitationRepository(
            IContext context,
            IPartyRepository partyRepository,
            IThreeDES des,
            IEventsProvider eventsProvider,
            IWorkshopsProvider workshopProvider,
            IHttpRequestHandler httpRequestHandler)
        {
            _customerBCOperation = new CustomerBCOperation();
            _customerOperation = new CustomerOperation();
            _invitationOperation = new InvitationOperation();
            _partyOperation = new PartyOperation();
            _consultantOperation = new ConsultantOperation();
            _eventOperation = new EventOperation();
            _unitOperation = new UnitOperation();
            _customerDetailOperation = new CustomerDetailOperation();
            _customerListOperation = new CustomerListOperation();
            _weCardCodeOperation = new WeChartCardCodeOperation();
            _uniteeOperation = new UniteeOperation();
            _context = context;
            _partyRepository = partyRepository;
            _threeDes = des;
            _eventsProvider = eventsProvider;
            _workshopProvider = workshopProvider;
            _httpRequestHandler = httpRequestHandler;
        }
        public object Accept(AcceptInvitation req)
        {
            #region check invitation logical
            InvitationTokenEntity token = _partyRepository.GetTokenEntity(req.InvitationToken);
            var party = _partyOperation.GetSingleDataByFunc(p => p.PartyKey == token.PartyKey);
            if (party == null)
            {
                throw new HttpError(HttpStatusCode.BadRequest, "ResourceError", ConfigurationManager.AppSettings["PartyNotExistsError"]);
            }
            var eventData = _eventOperation.GetSingleDataByFunc(e => e.EventKey == party.EventKey);
            if (DateTime.Now <= eventData.ApplicationEndDate)
            {
                throw new HttpError(HttpStatusCode.BadRequest, "ResourceError", ConfigurationManager.AppSettings["PartyNotInInvitationError"]);
            }
            if ((DateTime.Now > party.DisplayEndDate.Date && (!req.IsOnSite.HasValue || !req.IsOnSite.Value))
                || DateTime.Now > party.EndDate.Date)
            {
                throw new HttpError(HttpStatusCode.BadRequest, "ResourceError", ConfigurationManager.AppSettings["ExpiredInvitationError"]);
            }
            InvitationDataEntity invitation = _invitationOperation.GetSingleDataByFunc(i => i.PartyKey == token.PartyKey && i.PhoneNumber == req.PhoneNumber && i.Status != InvitationStatus.Cancelled);
            if (invitation != null)
            {
                throw new HttpError(HttpStatusCode.BadRequest,
                    "ResourceError",
                    _context.User.ContactID() != 0
                    ? ConfigurationManager.AppSettings["DuplicatedAcceptitionError"]
                    : ConfigurationManager.AppSettings["DuplicatedSelfAcceptitionError"]);
            }
            // owner is the creator of this customer by default            
            var result = GetCheckResponse(token, req.PhoneNumber);
            #endregion

            #region init customer entity
            CustomerEntity customer = new CustomerEntity();
            if (req.Age.HasValue)
            {
                customer.AgeRange = (int)req.Age.Value;
            }
            if (req.Career.HasValue)
            {
                customer.Career = (int)req.Career.Value;
            }
            customer.CreatedBy = _context.User.ContactID().ToString();
            customer.CustomerKey = Guid.NewGuid();
            customer.CustomerContactId = result.CustomerContactID;
            customer.dtCreated = DateTime.Now;
            customer.Inviter = party.AppliedContactID;
            customer.InviterName = party.AppliedName;
            customer.Level = result.Level.HasValue ? result.Level.Value.ToString() : null;
            customer.IsVIP = result.IsVIP;
            if (req.MaritalStatus.HasValue)
            {
                customer.MaritalStatus = (int)req.MaritalStatus.Value;
            }
            customer.Name = req.Name;
            customer.PhoneNumber = req.PhoneNumber;
            customer.UnionId = req.UnionId;
            if (!string.IsNullOrWhiteSpace(req.ReferenceBy))
            {
                long contactID;
                Guid refCustomerKey;
                if (long.TryParse(req.ReferenceBy, out contactID))
                {
                    var consultant = _consultantOperation.GetSingleDataByFunc(c => c.ContactID == contactID);
                    customer.Reference = string.Format("{0}{1}", consultant.LastName, consultant.FirstName);
                    customer.ReferenceKey = contactID.ToString();
                    customer.ReferenceType = 1;
                }
                else if (Guid.TryParse(req.ReferenceBy, out refCustomerKey))
                {
                    var refCustomer = _customerOperation.GetSingleDataByFunc(c => c.CustomerKey == refCustomerKey);
                    customer.Reference = refCustomer.Name;
                    customer.ReferenceKey = req.ReferenceBy;
                    customer.ReferenceType = 2;
                }
            }
            customer.IsNewer = false;
            customer.IsPromoted = false;
            customer.HasOrder = false;
            #endregion

            #region init invation
            InvitationDataEntity invitationData = new InvitationDataEntity
                {
                    CustomerKey = customer.CustomerKey,
                    InvitationKey = Guid.NewGuid(),
                    OwnerContactID = result.OwnerContactID.HasValue ? result.OwnerContactID.Value : 0,
                    OwnerUnitID = result.UnitID,
                    PartyKey = token.PartyKey.Value,
                    PhoneNumber = req.PhoneNumber,
                    Reference = customer.Reference,
                    ReferenceKey = customer.ReferenceKey,
                    ReferenceType = customer.ReferenceType,
                    Status = (req.IsOnSite.HasValue && req.IsOnSite.Value) ? InvitationStatus.OnSite : InvitationStatus.Accepted,
                    CheckinDate = (req.IsOnSite.HasValue && req.IsOnSite.Value) ? DateTime.Now : default(DateTime?)
                };
            #endregion

            _logger.Info(string.Format("Accept, {0}\t{1}\t{2}\t{3}\tReferenceBy: {4}", req.InvitationToken, token.CurrentUserContactID, token.UnitID, req.PhoneNumber, req.ReferenceBy));
            _customerOperation.CreateByTransaction(customer, (db, c) =>
            {
                _invitationOperation.Create(invitationData, db);
            });

            return new InvitationCommandResponse
            {
                ContactId = customer.CustomerContactId,
                Age = req.Age,
                Career = req.Career,
                CustomerKey = customer.CustomerKey,
                InvitationKey = invitationData.InvitationKey,
                Level = result.Level,
                IsVIP = result.IsVIP,
                MaritalStatus = req.MaritalStatus,
                Name = req.Name,
                PhoneNumber = req.PhoneNumber,
                InvationQRKey = _threeDes.Encrypt(string.Format(InvitationQRFormat, token.PartyKey, customer.CustomerKey, invitationData.InvitationKey))
            };
        }

        public object Cancel(CancelInvitation req)
        {
            var invitation = _invitationOperation.GetSingleDataByFunc(i => i.InvitationKey == req.InvitationKey && i.Status == InvitationStatus.Accepted);
            if (invitation == null)
            {
                throw new HttpError(HttpStatusCode.BadRequest, "ResourceError", ConfigurationManager.AppSettings["InvalidCancelOperationError"]);
            }
            var customer = _customerOperation.GetSingleDataByFunc(c => c.CustomerKey == invitation.CustomerKey);
            invitation.Status = InvitationStatus.Cancelled;
            var wecardCode = _weCardCodeOperation.GetSingleDataByFunc(wc => wc.InvitationKey == invitation.InvitationKey);
            if (wecardCode != null)
            {
                wecardCode.Status = (int)WeCardStatus.Cancelled;
            }
            _invitationOperation.UpdateByTransaction(invitation, (db, it) =>
            {
                if (wecardCode != null)
                {
                    _weCardCodeOperation.Update(wecardCode, db);
                }
            });

            //WechartToken wechartToken = GetWechartToken();
            //if (wechartToken != null && !string.IsNullOrWhiteSpace(wechartToken.OAuthData))
            //{
            //    Dictionary<string, string> httpParams = new Dictionary<string, string>();
            //    httpParams.Add("code", invitation.InvitationKey.ToString().ToLower());
            //    httpParams.Add("card_id", wecardCode.CardID);
            //    string url = string.Format(ConfigurationManager.AppSettings["CancelCardUrl"], wechartToken.OAuthData);
            //    var postRet = _httpRequestHandler.HttpPost<string>(url, httpParams);
            //    _logger.Info(string.Format("InvitationKey: {0},result: {1}", invitation.InvitationKey, postRet));
            //}

            InvitationCommandResponse result = new InvitationCommandResponse
            {
                CustomerKey = customer.CustomerKey,
                InvitationKey = invitation.InvitationKey,
                Level = string.IsNullOrWhiteSpace(customer.Level) ? default(int?) : int.Parse(customer.Level),
                IsVIP = customer.IsVIP,
                Name = customer.Name,
                PhoneNumber = customer.PhoneNumber,
                InvationQRKey = _threeDes.Encrypt(string.Format(InvitationQRFormat, invitation.PartyKey, customer.CustomerKey, invitation.InvitationKey))
            };
            AgeRange age;
            if (customer.AgeRange.HasValue && Enum.TryParse<AgeRange>(customer.AgeRange.ToString(), out age))
            {
                result.Age = age;
            }
            Career career;
            if (customer.Career.HasValue && Enum.TryParse<Career>(customer.Career.ToString(), out career))
            {
                result.Career = career;
            }
            MaritalStatusType maritalStatus;
            if (customer.MaritalStatus.HasValue && Enum.TryParse<MaritalStatusType>(customer.MaritalStatus.ToString(), out maritalStatus))
            {
                result.MaritalStatus = maritalStatus;
            }
            return result;
        }

        public object CheckCustomer(CheckCustomerInvitation req)
        {
            return GetCheckResponse(_partyRepository.GetTokenEntity(req.InvitationToken), req.PhoneNumber);
        }

        private CheckCustomerResponse GetCheckResponse(InvitationTokenEntity token, string phoneNumber)
        {
            var result = _customerBCOperation.ExecEntityProcedure("exec Usp_GetConsultantInfoByPhone @PhoneNumber", new { PhoneNumber = phoneNumber });
            CheckCustomerResponse resp = new CheckCustomerResponse();
            resp.UnitID = token.UnitID;
            resp.OwnerContactID = token.CurrentUserContactID;

            if (result != null && result.Any())
            {
                if (result[0].Level > 20)
                {
                    throw new HttpError(HttpStatusCode.BadRequest,
                        "ResourceError",
                        _context.User.ContactID() != 0
                        ? ConfigurationManager.AppSettings["PartyNotAllowedToAttend"]
                        : ConfigurationManager.AppSettings["PartyNotAllowedToSelfAttend"]);
                }

                //if (result[0].UnitID != token.UnitID)
                if (!CheckDIRInParty(result[0].DIRContactID.Value, token.PartyKey.Value))
                {
                    throw new HttpError(HttpStatusCode.BadRequest,
                        "ResourceError",
                        _context.User.ContactID() != 0
                        ? ConfigurationManager.AppSettings["UnitNotAllowedToAttend"]
                        : ConfigurationManager.AppSettings["UnitNotAllowedToSelfAttend"]);
                }

                resp.CustomerContactID = result[0].ContactID;
                resp.DIRContactID = result[0].DIRContactID;
                resp.DIRName = result[0].DIRName;
                resp.Level = result[0].Level;
                resp.RecuiterContactID = result[0].RecruiterContactID;
                resp.RecuiterName = result[0].RecruiterName;
                resp.IsVIP = result[0].Level < 15; // Level code < 15

                string msg = "该顾客为{0}的{1}，报名成功后将算作{0}邀请的顾客，不显示于您邀请的顾客名单中。";
                if (result[0].RecruiterContactID.HasValue && token.CurrentUserContactID != result[0].RecruiterContactID)
                {
                    resp.OwnerContactID = result[0].RecruiterContactID;
                    resp.Message = string.Format(msg, result[0].RecruiterName, resp.IsVIP ? "VIP顾客" : "美容顾问");
                    resp.UnitID = result[0].UnitID;
                }
                else if (result[0].DIRContactID.HasValue && token.CurrentUserContactID != result[0].RecruiterContactID)
                {
                    resp.OwnerContactID = result[0].DIRContactID;
                    resp.Message = string.Format(msg, result[0].DIRName, resp.IsVIP ? "VIP顾客" : "美容顾问");
                    resp.UnitID = result[0].UnitID;
                }
            }
            return resp;
        }

        public InvitationStatistics GetInvitationStatistics(Guid partyKey)
        {
            InvitationStatistics statistics = GetSigninStatistics(partyKey);
            return statistics;
        }

        private const string GetUnitInvitationSummary = "exec  GetUnitInvitationSummary @unitId,@partykey";
        public SigninStatistics GetSigninStatistics(Guid partyKey)
        {
            SigninStatistics statistics = new SigninStatistics();
            statistics.PartyKey = partyKey;
            statistics.PartyInviteesCount = (int)_invitationOperation.Count(i => i.PartyKey == partyKey && i.OwnerContactID != default(long));
            statistics.SelfInviteesCount = (int)_invitationOperation.Count(i => i.OwnerContactID == _context.ConsultantContactId && i.PartyKey == partyKey);
            statistics.UnitInviteesCount = (int)_invitationOperation.Count(i => i.OwnerUnitID == _context.User.Unit && i.PartyKey == partyKey);
            statistics.PartySigninCount = (int)_invitationOperation.Count(i => (i.Status == InvitationStatus.CheckedIn || i.Status == InvitationStatus.OnSite) && i.PartyKey == partyKey && i.OwnerContactID != default(long));

            var operation = new BaseEntityOperation<UnitInvitationSummary>(ConfigurationManager.AppSettings["Community"]);
            statistics.UnitInvitationSummary = operation.ExecEntityProcedure(GetUnitInvitationSummary, new { unitId = _context.User.Unit, partyKey = partyKey });
            // statistics.UnitInvitationSummary = _invitationOperation.SelectBySqlFmt<UnitInvitationSummary>(string.Format(GetUnitInvitationSummary, _context.User.Unit, partyKey));
            /*  statistics.UnitInvitationSummary = _invitationOperation.SelectBySqlFmt<UnitInvitationSummary>(
                 @"select	u.UnitID,
                                 uc.ContactID,
                                 uc.LastName + uc.FirstName as UnitName,
                                 case 
                                     when u.UnitID = {0} then 1
                                     else 0
                                 end as IsMyUnit,
                                 COUNT(1) AS [Count]
                     from iParty_Invitation ip with(nolock) 
                     inner join btc_UnitMember   u with(nolock) on ip.OwnerUnitID = u.UnitID
                     inner join ContactsLite.dbo.Consultants uc with(nolock) on u.Dir_ContactId = uc.ContactID
                     where ip.PartyKey = {1}
                     group by u.UnitID, uc.ContactID, uc.LastName + uc.FirstName ",
                 _context.User.Unit, partyKey); */

            return statistics;
        }

        public FeedbackStatistics GetFeedbackStatistics(Guid partyKey)
        {
            var signinStatistics = GetSigninStatistics(partyKey);
            var feedbackStatistics = new FeedbackStatistics();
            feedbackStatistics.PartyKey = signinStatistics.PartyKey;
            feedbackStatistics.UnitInviteesCount = signinStatistics.UnitInviteesCount;
            feedbackStatistics.SelfInviteesCount = signinStatistics.SelfInviteesCount;
            feedbackStatistics.PartyInviteesCount = signinStatistics.PartyInviteesCount;
            feedbackStatistics.UnitInvitationSummary = signinStatistics.UnitInvitationSummary;
            feedbackStatistics.PartySigninCount = signinStatistics.PartySigninCount;

            // get all the unit ids (applicant and unitee)
            var partyUnitIds = feedbackStatistics.UnitInvitationSummary.Select(u => u.UnitID);
            if(partyUnitIds == null)
                return feedbackStatistics;

            var customers = _invitationOperation.Join<CustomerListEntity, CustomerEntity, InvitationDataEntity>(
                (ide, ce) => ide.CustomerKey == ce.CustomerKey && ide.PartyKey == partyKey,
                ide => ide.PartyKey == partyKey); // get all customers of the party

            if (customers.Count > 0)
            {
                feedbackStatistics.NewComersCount = customers.Where(c => c.IsNewer).Count();
                feedbackStatistics.PromotionCount = customers.Where(c => c.IsPromoted).Count();
                feedbackStatistics.OrderedCustomerCount = customers.Where(c => c.HasOrder).Count();
            }

            return feedbackStatistics;
        }

        public List<SigninEntry> GetSignin()
        {
            long hostContactID;
            var currentDir = _context.ConsultantContactId;
            if (_context.User.IsNotDirector())
            {
                var dir = _unitOperation.GetSingleDataByFunc(c => c.UnitID == _context.User.Unit);
                currentDir = dir.Director;
            }

            var unionpartyitem = _partyOperation.Join<PartyEntity, UniteeEntity>((p, u) => p.PartyKey == u.PartyKey
                && u.UniteeContactID == currentDir);

            unionpartyitem = unionpartyitem.Where(u => DateTime.Now.Date >= u.DisplayStartDate.Date && DateTime.Now.Date<= u.DisplayEndDate.Date).ToList();
            if (unionpartyitem == null || unionpartyitem.Count == 0)
            {
                hostContactID = currentDir;
            }
            else
            {

                hostContactID = unionpartyitem.FirstOrDefault().AppliedContactID;
            }
            var parties = _partyOperation.GetDataByFunc(p => p.AppliedContactID == hostContactID);
            parties = parties.Where(p => DateTime.Now.Date >= p.DisplayStartDate.Date && DateTime.Now.Date <= p.DisplayEndDate.Date).ToList();
            var result = new List<SigninEntry>();
            var addParty = new List<Guid>();
            foreach (var item in parties)
            {
                if (addParty.Contains(item.PartyKey))
                {
                    continue;
                }
                var party = _partyOperation.GetSingleDataByFunc(p => p.PartyKey == item.PartyKey);
                var events = _eventsProvider.GetDetailBy(party.EventKey).FirstOrDefault();
                var workShop = _workshopProvider.GetWorkshopById(party.WorkshopId, party.EventKey);
                var statistics = GetSigninStatistics(item.PartyKey);
                addParty.Add(item.PartyKey);
                result.Add(new SigninEntry
                {
                    EventKey = events.EventKey,
                    Category = events.Category,
                    PartyEndDate = party.EndDate,
                    PartyStartDate = party.StartDate,
                    PartyDisplayEndDate = party.DisplayEndDate,
                    PartyDisplayStartDate = party.DisplayStartDate,
                    PartyInviteesCount = statistics.PartyInviteesCount,
                    PartyKey = item.PartyKey,
                    Title = events.Title,
                    WorkshopLocation = workShop.Location,
                    WorkshopName = workShop.Name,
                    AppliedContactId = party.AppliedContactID,
                    AppliedName = party.AppliedName,
                    PartySigninCount = statistics.PartySigninCount,
                    SelfUnitID = _context.User.Unit,
                    UnitInviteesCount = statistics.UnitInviteesCount
                });
            }
            return result;
        }

        public object CheckIn(InvitationCheckIn req)
        {
            if(_context.User.Level < 15)
            {
                throw new HttpError(HttpStatusCode.BadRequest, "ResourceError", ConfigurationManager.AppSettings["InvalidSignInLevel"]);
            }

            var invitation = _invitationOperation.GetSingleDataByFunc(i => i.InvitationKey == req.InvitationKey);
            if(invitation == null)
            {
                throw new HttpError(HttpStatusCode.BadRequest, "InvalidSignInError", ConfigurationManager.AppSettings["InvalidSignInOperation"]);
            }
            else
            {
                var currentDir = _context.ConsultantContactId;
                if (_context.User.IsNotDirector())
                {
                    var dir = _unitOperation.GetSingleDataByFunc(c => c.UnitID == _context.User.Unit);
                    currentDir = dir.Director;
                }

                if (!CheckDIRInParty(currentDir, invitation.PartyKey))
                {
                    throw new HttpError(HttpStatusCode.BadRequest, "ResourceError", ConfigurationManager.AppSettings["InvalidSignInOperation"]);
                }

                if(invitation.Status == InvitationStatus.CheckedIn || invitation.Status == InvitationStatus.OnSite)
                {
                    throw new HttpError(HttpStatusCode.BadRequest, "DuplCheckInError", ConfigurationManager.AppSettings["DuplicatedSignInOperation"]);
                }

                if(invitation.Status != InvitationStatus.Accepted)
                {
                    throw new HttpError(HttpStatusCode.BadRequest, "InvalidSignInError", ConfigurationManager.AppSettings["InvalidSignInOperation"]);
                }
            }

            var party = _partyOperation.GetSingleDataByFunc(p => p.PartyKey == invitation.PartyKey);
            if (party == null || (party.StartDate > DateTime.Now || party.EndDate < DateTime.Now))
            {
                throw new HttpError(HttpStatusCode.BadRequest, "ResourceError", ConfigurationManager.AppSettings["InvalidSignInOperation"]);
            }

            var customer = _customerOperation.GetSingleDataByFunc(c => c.CustomerKey == invitation.CustomerKey);
            var wecardCode = _weCardCodeOperation.GetSingleDataByFunc(wc => wc.InvitationKey == invitation.InvitationKey);
            if (wecardCode != null)
            {
                wecardCode.Status = (int)WeCardStatus.CheckedIn;
            }

            invitation.CheckInType = req.CheckInType;
            invitation.CheckinDate = DateTime.Now;
            invitation.Status = InvitationStatus.CheckedIn;
            _invitationOperation.UpdateByTransaction(invitation, (db, it) =>
            {
                if (wecardCode != null)
                {
                    _weCardCodeOperation.Update(wecardCode, db);
                }
            });

            //WechartToken wechartToken = GetWechartToken();
            //if (wechartToken != null && !string.IsNullOrWhiteSpace(wechartToken.OAuthData))
            //{
            //    Dictionary<string, string> httpParams = new Dictionary<string, string>();
            //    httpParams.Add("code", invitation.InvitationKey.ToString().ToLower());
            //    httpParams.Add("card_id", wecardCode.CardID);
            //    string url = string.Format(ConfigurationManager.AppSettings["ConsumeCardUrl"], System.Web.HttpUtility.UrlEncode(wechartToken.OAuthData));
            //    var postRet = _httpRequestHandler.HttpPost<string>(url, httpParams);
            //    _logger.Info(string.Format("InvitationKey: {0},result: {1}", invitation.InvitationKey, postRet));
            //}

            var result = new CheckInResponse
            {
                CustomerKey = invitation.CustomerKey,
                PartyKey = invitation.PartyKey,
                InvitationKey = invitation.InvitationKey,
                LevelOnAccept = string.IsNullOrWhiteSpace(customer.Level) ? "0" : customer.Level,
                IsVIP = customer.IsVIP,
                Name = customer.Name,
                PhoneNumber = invitation.PhoneNumber
            };
            AgeRange age;
            if (customer.AgeRange.HasValue && Enum.TryParse<AgeRange>(customer.AgeRange.ToString(), out age))
            {
                result.Age = age;
            }
            Career career;
            if (customer.Career.HasValue && Enum.TryParse<Career>(customer.Career.ToString(), out career))
            {
                result.Career = career;
            }
            MaritalStatusType maritalStatus;
            if (customer.MaritalStatus.HasValue && Enum.TryParse<MaritalStatusType>(customer.MaritalStatus.ToString(), out maritalStatus))
            {
                result.MaritalStatus = maritalStatus;
            }
            return result;
        }

        public CustomerDetailResponse GetCustomerDetail(CustomerDetail dto)
        {
            var details = _invitationOperation.Join<CustomerDetailEntity, CustomerEntity, InvitationDataEntity>(
                (ide, ce) => ide.CustomerKey == ce.CustomerKey && ide.InvitationKey == dto.InvitationKey,
                ide => ide.InvitationKey == dto.InvitationKey);
            if (details == null || details.Count <= 0)
            {
                throw new HttpError(HttpStatusCode.BadRequest, "ResourceError", ConfigurationManager.AppSettings["NoCustomerError"]);
            }
            var detail = details[0];
            var consultant = _consultantOperation.GetSingleDataByFunc(c => c.ContactID == detail.OwnerContactID);
            var result = new CustomerDetailResponse
            {
                IsVIP = detail.IsVIP,
                CustomerKey = detail.CustomerKey,
                FullName = detail.Name,
                InvitationKey = detail.InvitationKey,
                OwnerContactID = detail.OwnerContactID,
                OwnerName = string.Format("{0},{1}", consultant.LastName, consultant.FirstName),
                PartyKey = detail.PartyKey,
                PhoneNumber = detail.PhoneNumber,
                Status = detail.Status,
                dtCreated = detail.dtCreated
            };
            if (detail.AgeRange.HasValue)
            {
                result.AgeRange = (AgeRange)detail.AgeRange.Value;
            }
            if (detail.Career.HasValue)
            {
                result.Career = (Career)detail.Career.Value;
            }
            if (detail.MaritalStatus.HasValue)
            {
                result.MaritalStatus = (MaritalStatusType)detail.MaritalStatus.Value;
            }
            return result;
        }

        public IEnumerable<QueryCustomerResponse> GetCustomerList(QueryCustomer dto)
        {
            var customers = _invitationOperation.Join<CustomerListEntity, CustomerEntity, InvitationDataEntity>(
                (ide, ce) => ide.CustomerKey == ce.CustomerKey && ide.PartyKey == dto.PartyKey && ide.OwnerUnitID == _context.User.Unit,
                ide => ide.PartyKey == dto.PartyKey && ide.OwnerUnitID == _context.User.Unit);
            if (customers.Count > 0)
            {
                var ownerContacts = from c in customers
                                    select c.OwnerContactID;
                var consultants = _consultantOperation.GetDataByFunc(c => Sql.In(c.ContactID, ownerContacts));

                var result = from cm in customers
                             join c in consultants on cm.OwnerContactID equals c.ContactID into consultantGroup
                             from c in consultantGroup.DefaultIfEmpty()
                             select new QueryCustomerResponse
                                   {
                                       ContactId = cm.CustomerContactId,
                                       OwnerContactID = cm.OwnerContactID,
                                       CustomerKey = cm.CustomerKey,
                                       FullName = cm.Name,
                                       InvitationKey = cm.InvitationKey,
                                       OwnerName = c != null ? string.Format("{0},{1}", c.LastName, c.FirstName) : "",
                                       LevelOnAccept = string.IsNullOrWhiteSpace(cm.Level) ? "0" : cm.Level, // the customer level on accept invitation day, not current level of customer
                                       PartyKey = cm.PartyKey,
                                       PhoneNumber = cm.PhoneNumber,
                                       Status = cm.Status,
                                       dtCreated = cm.dtCreated,
                                       IsNewer = cm.IsNewer,
                                       IsPromoted = cm.IsPromoted,
                                       HasOrder = cm.HasOrder
                                   };

                return result;
            }
            return new List<QueryCustomerResponse>();
        }

        WechartToken GetWechartToken()
        {
            var result = _httpRequestHandler.HttpGet<WechartToken>(ConfigurationManager.AppSettings["AccessTokenUrl"]);
            return result;
        }

        private bool IsPromotion(int levelBefore, int levelAfter)
        {
            if (levelBefore >= levelAfter || levelAfter < 15)
                return false;

            return true;
        }

        private bool CheckDIRInParty(long dirContactID, Guid partykey)
        {
            var parties = _partyOperation.GetDataByFunc(p => p.PartyKey == partykey && p.AppliedContactID == dirContactID);
            if (parties != null && parties.Any())
                return true;
            var unitee = _uniteeOperation.GetDataByFunc(u => u.PartyKey == partykey && u.UniteeContactID == dirContactID);
            if (unitee != null && unitee.Any())
                return true;

            return false;
        }
    }

    public class WechartToken
    {
        public string OAuthData { get; set; }
    }
}
