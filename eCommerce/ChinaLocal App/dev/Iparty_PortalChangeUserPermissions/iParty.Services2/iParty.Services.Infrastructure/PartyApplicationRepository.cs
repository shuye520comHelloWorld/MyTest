using System;
using iParty.Services.Contract;
using iParty.Services.Interface;
using iParty.Services.ORM.Operations;
using QuartES.Services.Core;
using System.Configuration;
using iParty.Services.Entity;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using ServiceStack.OrmLite;
using iParty.Services.Interface.Exception;
using ServiceStack;
using System.Net;
using NLog;

namespace iParty.Services.Infrastructure
{
    public sealed class PartyApplicationRepository : IPartyApplicationRepository
    {
        readonly Logger _logger = LogManager.GetCurrentClassLogger();
        private PartyOperation partyOperation;
        private PartyInfoOperation partyInfoOperation;
        private UniteeOperation uniteeOperation;
        private ConsultantOperation consultantOperation;
        private InvitationOperation _invitationOperation;
        private IContext _context;
        private IWorkshopsProvider workshopProvider;
        private IEventsProvider _eventsProvider;
        private IPartyRepository _partyReposity;
        private IInvitationRepository _invitationRepository;
        private UnitOperation _unitOperation;
        public PartyApplicationRepository(
            IContext context,
            IWorkshopsProvider workshop,
            IEventsProvider eventsProvider,
            IPartyRepository partyReposity,
            IInvitationRepository invitationRepository)
        {
            partyOperation = new PartyOperation();
            partyInfoOperation = new PartyInfoOperation();
            uniteeOperation = new UniteeOperation();
            consultantOperation = new ConsultantOperation();
            _invitationOperation = new InvitationOperation();
            _unitOperation = new UnitOperation();
            this._context = context;
            workshopProvider = workshop;
            this._eventsProvider = eventsProvider;
            _partyReposity = partyReposity;
            _invitationRepository = invitationRepository;
        }

        private const string GetCoHostApplications = "exec ipartySP_GetCoHostApplications @contactId";
        public List<ApplicationEntry> Get()
        {
            var applications = partyOperation.Join<PartyInfo, EventEntity>((p, e) => p.EventKey == e.EventKey
                && p.AppliedContactID == _context.ConsultantContactId);
            var result = new List<ApplicationEntry>();
            foreach (var item in applications)
            {
                var application = BuildApplicationEntry(item, true);
                result.Add(application);
                // application.WorkshopLocation = 
            }
            var coHostApplicaitons = partyInfoOperation.ExecEntityProcedure(GetCoHostApplications, new { contactId = _context.ConsultantContactId });
            foreach (var item in coHostApplicaitons)
            {
                var application = BuildApplicationEntry(item, false);
                result.Add(application);
            }
            return result;
        }

        public List<ApplicationEntry> GetHistory()
        {
            var currentDir = _context.ConsultantContactId;
            var isDir = _context.User.IsDirector();
            if (!isDir)
            {
                var dir = _unitOperation.GetSingleDataByFunc(c => c.UnitID == _context.User.Unit);
                currentDir = dir.Director;
            }

            var applications = partyOperation.Join<PartyInfo, EventEntity>((p, e) => p.EventKey == e.EventKey
                && p.AppliedContactID == currentDir && DateTime.Now.Date > p.DisplayEndDate);

            var result = new List<ApplicationEntry>();
            foreach (var item in applications)
            {
                var application = BuildApplicationEntry(item, isDir);
                result.Add(application);
            }
            var coHostApplicaitons = partyInfoOperation.ExecEntityProcedure(GetCoHostApplications, new { contactId = currentDir });
            foreach (var item in coHostApplicaitons)
            {
                if (DateTime.Now.Date > item.DisplayEndDate)
                {
                    var application = BuildApplicationEntry(item, false);
                    result.Add(application);
                }
            }
            return result;
        }



        private ApplicationEntry BuildApplicationEntry(PartyInfo item, bool CanEdit)
        {
            var application = new ApplicationEntry();
            application.EventKey = item.EventKey; 
            application.isOwner = CanEdit;
            application.AppliedContactId = item.AppliedContactID;
            application.AppliedName = item.AppliedName;
            var eventinfo = _eventsProvider.GetDetailBy(item.EventKey).FirstOrDefault();
            application.Title = eventinfo.Title;
            application.isApplicationEnd = DateTime.Now > eventinfo.ApplicationEndDate;
            application.PartyKey = item.PartyKey;
            application.PartyEndDate = item.EndDate;
            application.PartyStartDate = item.StartDate;
            application.PartyDisplayEndDate = item.DisplayEndDate;
            application.PartyDisplayStartDate = item.DisplayStartDate;
            application.FeadbackEndDate = item.FeedbackEndDate; 
            application.PartyType = item.PartyType == 0 ? PartyType.Party : PartyType.ThanksGiving;
            application.Category = eventinfo.Category;
            application.IsSelfHost = item.OrganizationType == 0;
            application.WorkShopId = item.WorkshopId;
            var workShop = workshopProvider.GetWorkshopById(application.WorkShopId, item.EventKey);
            if (workShop != null)
            {
                application.WorkshopLocation = workShop.Location;
                application.WorkshopName = workShop.Name;
            }
            application.InvitationCount = _invitationOperation.Count(x => x.PartyKey == item.PartyKey && x.OwnerContactID != default(long));
            application.CheckInCount = _invitationOperation.Count(x => x.PartyKey == item.PartyKey && x.OwnerContactID != default(long) && (x.Status == InvitationStatus.CheckedIn || x.Status == InvitationStatus.OnSite));
            return application;
        }

        public List<ApplicationEntryWithDetail> GetDetail(Guid PartyKey)
        {
            var party = partyOperation.GetDataByFunc(p => p.PartyKey == PartyKey).FirstOrDefault();
            var applicationlist = new List<ApplicationEntryWithDetail>();

            if (party == null)
                return applicationlist;

            if(party.AppliedContactID == _context.ConsultantContactId)
            {
                var application = BuildApplicationEntryDetail(party, true);
                applicationlist.Add(application);
            }
            else
            {
                var coHostApplicaiton = uniteeOperation.GetDataByFunc(u => u.PartyKey == PartyKey && u.UniteeContactID == _context.ConsultantContactId);
                if(coHostApplicaiton != null && coHostApplicaiton.Any())
                {
                    var application = BuildApplicationEntryDetail(party, false);
                    applicationlist.Add(application);
                }
            }

            return applicationlist;
        }

        private ApplicationEntryWithDetail BuildApplicationEntryDetail(PartyEntity item, bool canEdit)
        {
            var application = new ApplicationEntryWithDetail();
            application.EventKey = item.EventKey;
            application.PartyKey = item.PartyKey;
            application.isOwner = canEdit;
            application.AppliedContactId = item.AppliedContactID;
            application.AppliedName = item.AppliedName;
            application.PartyEndDate = item.EndDate;
            application.PartyStartDate = item.StartDate;
            application.PartyDisplayEndDate = item.DisplayEndDate;
            application.PartyDisplayStartDate = item.DisplayStartDate;
            application.IsSelfHost = item.OrganizationType == 0;
            application.WorkShopId = item.WorkshopId;

            var workShop = workshopProvider.GetWorkshopById(application.WorkShopId, item.EventKey);
            application.WorkshopLocation = workShop.Location;
            application.WorkshopName = workShop.Name;
            var eventinfo = _eventsProvider.GetDetailBy(item.EventKey).FirstOrDefault();
            application.Title = eventinfo.Title;
            application.isApplicationEnd = DateTime.Now > eventinfo.ApplicationEndDate;
            application.ApplicationEndDate = eventinfo.ApplicationEndDate;
            application.ApplicationStartDate = eventinfo.ApplicationStartDate;
            application.PartyType = item.PartyType == 0 ? PartyType.Party : PartyType.ThanksGiving;
            application.PartyAllowStartDate = eventinfo.PartyAllowStartDate;
            application.PartyAllowEndDate = eventinfo.PartyAllowEndDate;
            application.Category = eventinfo.Category;
            application.Description = eventinfo.Description;
            application.CoHostConsultants = GetCoHostConsutlants(item.PartyKey);
            application.EventEndDate = eventinfo.EventEndDate;
            application.EventStartDate = eventinfo.EventStartDate;
            application.Notes = eventinfo.Notes;
            return application;
        }

        public IEnumerable<Consultant> GetCoHostConsutlants(Guid partyKey)
        {
            var clist = uniteeOperation.GetDataByFunc(t => t.PartyKey == partyKey);
            var consultants = new List<Consultant>();
            foreach (var item in clist)
            {
                var consultant = new Consultant();
                consultant.ContactId = item.UniteeContactID;
                consultant.FullName = item.FullName;
                consultant.Level = item.LeveLID;
                consultant.StartDate = item.ConsultantStartDate;
                consultant.UnitId = item.UnitId;
                consultants.Add(consultant);

            }
            return consultants;
        }

        private string coHostConsultantMessageFormat = ConfigurationManager.AppSettings["coHostConsultantMessageFormat"];
        private string coHostConsultantsUpdateMessageFormat = ConfigurationManager.AppSettings["coHostConsultantsUpdateMessageFormat"];
        private string coHostConsultantDeleteMessageFormat = ConfigurationManager.AppSettings["coHostConsultantDeleteMessageFormat"];
        private int smsType = int.Parse(ConfigurationManager.AppSettings["smsType"]);
        private string strGetCoHostApplications = "exec GetCoHostApplications @contactId,@EventKey";
        public object Create(CreatePartyApplication dto)
        {
            // verify the  the Role 
            if (!_context.User.IsDirector())
            {
                throw new HttpError(HttpStatusCode.BadRequest, "RoleError", ConfigurationManager.AppSettings["DirectorRoleError"]);
            }
            // verify the  the event 
            var eventDetail = validateEvent(dto.EventKey);
            if (eventDetail.ApplicationStartDate > DateTime.Now || eventDetail.ApplicationEndDate < DateTime.Now)
            {
                throw new HttpError(HttpStatusCode.BadRequest, "ResourceError", ConfigurationManager.AppSettings["AppliedDateError"]);
            }

            validateApplicationDuration(dto.StartDateUtc, dto.EndDateUtc, eventDetail);

            var applications = partyOperation.GetDataByFunc(t => t.AppliedContactID == _context.ConsultantContactId && t.EventKey == dto.EventKey);
            // if this user has already applied this event . throw a expception to user
            if (applications != null && applications.Count > 0)
            {
                throw new HttpError(HttpStatusCode.BadRequest, "ResourceError", ConfigurationManager.AppSettings["EventAppliedError"]);
            }

            var coHostApplications = partyOperation.ExecEntityProcedure(strGetCoHostApplications, new { contactId = _context.ConsultantContactId, EventKey = dto.EventKey });

            if (coHostApplications != null && coHostApplications.Count > 0)
            {
                throw new HttpError(HttpStatusCode.BadRequest, "ResourceError", ConfigurationManager.AppSettings["EventAppliedCoHostError"]);
            }

            //  applications = partyOperation.GetDataByFunc(t => t.WorkshopId == dto.WorkshopId && t.AppliedContactID != _context.ConsultantContactId);

            // there is only one party hold on at the same time
            var sameWorkShopApplications = partyOperation.GetDataByFunc(t => t.WorkshopId == dto.WorkshopId);
            foreach (var application in sameWorkShopApplications)
            {

                if (dto.EndDateUtc <= application.DisplayStartDate || dto.StartDateUtc >= application.DisplayEndDate)
                {
                    continue;
                }

                throw new HttpError(HttpStatusCode.BadRequest, "ResourceError", ConfigurationManager.AppSettings["WorkShopDateError"]);


            }


            var createItem = new PartyEntity();
            createItem.PartyKey = Guid.NewGuid();
            createItem.AppliedContactID = _context.ConsultantContactId;
            var profileOpeation = new ProfileOpeation();
            var result = profileOpeation.ExecEntityProcedure("exec usp_ProfileGet @ContactID", new { ContactID = createItem.AppliedContactID });

            createItem.AppliedName = string.Format("{0},{1}", result.FirstOrDefault().LastName, result.FirstOrDefault().FirstName);
            createItem.EndDate = eventDetail.PartyAllowEndDate; // actual date time
            createItem.StartDate = eventDetail.PartyAllowStartDate; // actual date time
            createItem.DisplayEndDate = dto.EndDateUtc; // display date time is the applied date time
            createItem.DisplayStartDate = dto.StartDateUtc; // display date time is the applied date time
            createItem.EventKey = dto.EventKey;
            createItem.OrganizationType = (int)OrganizationType.Standalone;
            if (dto.UnionConsultants != null && dto.UnionConsultants.Count >= 1)
            {
                createItem.OrganizationType = (int)OrganizationType.Joint;
            }
            createItem.PartyKey = Guid.NewGuid();
            createItem.WorkshopId = dto.WorkshopId;
            createItem.CreateDate = DateTime.Now;

            // get the workshop info from db

            Workshop workShop = GetMyWorkShop(dto.WorkshopId, dto.EventKey);
            if (workShop == null)
            {
                throw new HttpError(HttpStatusCode.BadRequest, "ResourceError", ConfigurationManager.AppSettings["WorkShopError"]);
            }
            createItem.PartyType = (int)workShop.Type;
            var coHostConsutlantIdList = new Dictionary<long, bool>();
            foreach (var item in workShop.Consultants)
            {
                if (!coHostConsutlantIdList.ContainsKey(item.ContactId))
                    coHostConsutlantIdList.Add(item.ContactId, item.IsApplied);
            }

            if (dto.UnionConsultants != null && dto.UnionConsultants.Count >= 1)
            {
                foreach (var c in dto.UnionConsultants)
                {
                    if (!coHostConsutlantIdList.ContainsKey(c))
                    {
                        throw new HttpError(HttpStatusCode.BadRequest, "ResourceError", ConfigurationManager.AppSettings["CoHostError"]);

                    }

                    if (coHostConsutlantIdList.ContainsKey(c) && coHostConsutlantIdList[c] == true)
                    {
                        var consultant = workShop.Consultants.First(wc => wc.ContactId == c);
                        throw new HttpError(HttpStatusCode.BadRequest, "ResourceError", string.Format(ConfigurationManager.AppSettings["CoHostHasPartyError"], consultant.LastName + consultant.FirstName));
                    }
                }
                DealWithUnitee(createItem.PartyKey, dto.UnionConsultants);
            }


            partyOperation.Create(createItem);
            if (dto.UnionConsultants != null && dto.UnionConsultants.Count >= 1)
            {
                foreach (var c in dto.UnionConsultants)
                {
                    var dateString = string.Format("{0}年{1}月{2}日", createItem.DisplayStartDate.Year, createItem.DisplayStartDate.Month, createItem.DisplayStartDate.Day);
                    var strtype = "礼遇日";
                    if (createItem.PartyType == 0)
                    {
                        strtype = "爱PARTY";
                    }
                    var msg = string.Format(coHostConsultantMessageFormat, createItem.AppliedName, dateString, strtype);
                    MessageSender.Send(c, msg, smsType);
                }


            }



            return new PartyApplicationCommandResponse
            {
                PartyKey = createItem.PartyKey,
                EventKey = createItem.EventKey,
                CreateDateUtc = createItem.CreateDate
            };
        }



        private static void validateApplicationDuration(DateTime StartDateUtc, DateTime EndDateUtc, EventEntryWithDetail eventDetail)
        {

            if (StartDateUtc < eventDetail.PartyAllowStartDate || StartDateUtc < eventDetail.EventStartDate)
            {
                throw new HttpError(HttpStatusCode.BadRequest, "ResourceError", ConfigurationManager.AppSettings["StartDateError"]);

            }
            if (EndDateUtc > eventDetail.PartyAllowEndDate || EndDateUtc < eventDetail.EventStartDate)
            {
                throw new HttpError(HttpStatusCode.BadRequest, "ResourceError", ConfigurationManager.AppSettings["EndDateError"]);

            }
        }

        private Workshop GetMyWorkShop(long WorkshopId, Guid eventKey)
        {
            var workshops = workshopProvider.GetWorkshops(eventKey);

            Workshop workShop = null;
            foreach (var item in workshops)
            {
                if (item.WorkshopId == WorkshopId)
                {
                    workShop = item;
                    break;
                }
            }
            return workShop;
        }


        public object Update(UpdatePartyApplication dto)
        {
            // verify the  the Role 
            if (!_context.User.IsDirector())
            {
                throw new HttpError(HttpStatusCode.BadRequest, "ResourceError", ConfigurationManager.AppSettings["DirectorRoleError"]);

            }


            var party = partyOperation.GetSingleData("PartyKey", dto.PartyKey);
            if (party == null)
            {
                throw new HttpError(HttpStatusCode.BadRequest, "ResourceError", ConfigurationManager.AppSettings["PartyError"]);

            }

            var sameWorkShopApplications = partyOperation.GetDataByFunc(t => t.WorkshopId == party.WorkshopId && t.PartyKey != party.PartyKey);
            foreach (var application in sameWorkShopApplications)
            {
                if (dto.EndDateUtc <= application.DisplayStartDate || dto.StartDateUtc >= application.DisplayEndDate)
                {
                    continue;
                }

                throw new HttpError(HttpStatusCode.BadRequest, "ResourceError", ConfigurationManager.AppSettings["WorkShopDateError"]);
            }
            var eventDetail = validateEvent(party.EventKey);
            validateApplicationDuration(dto.StartDateUtc, dto.EndDateUtc, eventDetail);
            party.DisplayStartDate = dto.StartDateUtc;
            party.DisplayEndDate = dto.EndDateUtc;
            party.OrganizationType = (int)OrganizationType.Standalone;

            Workshop workShop = GetMyWorkShop(party.WorkshopId, party.EventKey);
            var coHostConsutlantIdList = new Dictionary<long, bool>();
            foreach (var item in workShop.Consultants)
            {
                if (!coHostConsutlantIdList.ContainsKey(item.ContactId))
                    coHostConsutlantIdList.Add(item.ContactId, item.IsApplied);
            }
            var preCoHostConsultants = GetCoHostConsutlants(dto.PartyKey);
            if (dto.UnionConsultants != null && dto.UnionConsultants.Count >= 1)
            {
                foreach (var c in dto.UnionConsultants)
                {
                    if (!coHostConsutlantIdList.ContainsKey(c))
                    {
                        throw new HttpError(HttpStatusCode.BadRequest, "ResourceError", ConfigurationManager.AppSettings["CoHostError"]);

                    }

                    if (coHostConsutlantIdList.ContainsKey(c) // if the dir is in the workshop
                        && coHostConsutlantIdList[c] == true  // but the dir has joined a party
                        && preCoHostConsultants.FirstOrDefault(pc => pc.ContactId == c) == null) // exclude that party is current one
                    {
                        var consultant = workShop.Consultants.First(wc => wc.ContactId == c);
                        throw new HttpError(HttpStatusCode.BadRequest, "ResourceError", string.Format(ConfigurationManager.AppSettings["CoHostHasPartyError"], consultant.LastName + consultant.FirstName));
                    }
                }
                party.OrganizationType = (int)OrganizationType.Joint;


                DealWithUnitee(dto.PartyKey, dto.UnionConsultants);

            }
            else
            {
                // remove all coHostConsutlants
                uniteeOperation.DeleteByFunc(u => u.PartyKey == party.PartyKey);
            }
            // save the changes into db
            partyOperation.Update(party);
            var preConsultantIdList = new List<long>();
            foreach (var item in preCoHostConsultants)
            {
                preConsultantIdList.Add(item.ContactId);
            }

            if (dto.UnionConsultants == null)
            {
                dto.UnionConsultants = new List<long>();
            }
            foreach (var pc in preCoHostConsultants)
            {

                if (!dto.UnionConsultants.Contains(pc.ContactId))
                {
                    var dateString = string.Format("{0}年{1}月{2}日", party.DisplayStartDate.Year, party.DisplayStartDate.Month, party.DisplayStartDate.Day);
                    var strtype = "礼遇日";
                    if (party.PartyType == 0)
                    {
                        strtype = "爱PARTY";
                    }
                    var msg = string.Format(coHostConsultantsUpdateMessageFormat, party.AppliedName, dateString, strtype);

                    MessageSender.Send(pc.ContactId, msg, smsType);
                    // informConsultants.Add(pc.ContactId);
                }
            }
            if (dto.UnionConsultants != null)
            {
                foreach (var item in dto.UnionConsultants)
                {
                    if (!preConsultantIdList.Contains(item))
                    {

                        var dateString = string.Format("{0}年{1}月{2}日", party.DisplayStartDate.Year, party.DisplayStartDate.Month, party.DisplayStartDate.Day);
                        var strtype = "礼遇日";
                        if (party.PartyType == 0)
                        {
                            strtype = "爱PARTY";
                        }
                        var msg = string.Format(coHostConsultantMessageFormat, party.AppliedName, dateString, strtype);

                        MessageSender.Send(item, msg, smsType);
                    }
                }
            }


            return new PartyApplicationCommandResponse
            {
                PartyKey = party.PartyKey,
                EventKey = party.EventKey,
                CreateDateUtc = party.CreateDate
            };
        }

        private EventEntryWithDetail validateEvent(Guid eventKey)
        {
            var eventItems = _eventsProvider.GetDetailBy(eventKey);
            if (eventItems == null || eventItems.Count() == 0)
            {
                throw new HttpError(HttpStatusCode.BadRequest, "ResourceError", ConfigurationManager.AppSettings["EventKeyError"]);

            }
            var eventItem = eventItems.FirstOrDefault();
            if (eventItem.ApplicationEndDate < DateTime.Now)
            {
                throw new HttpError(HttpStatusCode.BadRequest, "ResourceError", ConfigurationManager.AppSettings["EventDateError"]);

            }
            if (eventItem.ApplicationStartDate > DateTime.Now)
            {
                throw new HttpError(HttpStatusCode.BadRequest, "ResourceError", ConfigurationManager.AppSettings["EventDateError"]);

            }

            return eventItem;
        }

        private void DealWithUnitee(Guid partyKey, IEnumerable<long> uniteeConsultants)
        {
            // get the consultants info from the contactslite db 
            var consultants = consultantOperation.GetDataByFunc(c => Sql.In(c.ContactID, uniteeConsultants));
            var unitees = from uc in consultants
                          select new UniteeEntity
                          {
                              UniteeContactID = uc.ContactID,
                              UnitId = uc.UnitID,
                              PartyKey = partyKey,
                              FullName = string.Format("{0},{1}", uc.LastName.Trim(), uc.FirstName.Trim()),
                              LeveLID = uc.Level,
                              ConsultantStartDate = uc.StartDate,
                              CreatedBy = _context.User.ContactID().ToString(),
                              CreatedDate = DateTime.Now
                          };
            // check the levelCode and startDate
            //foreach (var item in unitees)
            //{
            //    if (item.LeveLID > _context.User.Level)
            //    {
            //        throw new HttpError(HttpStatusCode.BadRequest, "ResourceError", string.Format("coHostConsultant {0} level is higher !", item.UniteeContactID.ToString()));

            //    }
            //    else if (item.LeveLID == _context.User.Level && item.ConsultantStartDate < _context.User.StartDate())
            //    {
            //        throw new HttpError(HttpStatusCode.BadRequest, "ResourceError", string.Format("coHostConsultant {0} startDate is ealier !", item.UniteeContactID.ToString()));

            //    }
            //}
            uniteeOperation.DeleteByFunc(u => u.PartyKey == partyKey);
            uniteeOperation.CreateBatchData(unitees);
        }
        /// <summary>
        /// delete the application 
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public object Delete(DeletePartyApplication dto)
        {
            // verify the  the Role 
            if (!_context.User.IsDirector())
            {
                throw new HttpError(HttpStatusCode.BadRequest, "RoleError", ConfigurationManager.AppSettings["DirectorRoleError"]);
                //throw new InvalidRequestException("you are not a director.");
            }

            var party = partyOperation.GetSingleData("PartyKey", dto.PartyKey);
            if (party == null || party.AppliedContactID != _context.User.ContactID())
            {
                throw new HttpError(HttpStatusCode.BadRequest, "ResourceError", ConfigurationManager.AppSettings["DeleteError"]);
            }


            if (party.OrganizationType == 1)
            {
                var preCoHostConsultants = GetCoHostConsutlants(party.PartyKey);
                if (preCoHostConsultants != null && preCoHostConsultants.Any())
                {
                    uniteeOperation.DeleteByFunc(u => u.PartyKey == dto.PartyKey);

                    foreach (var pc in preCoHostConsultants)
                    {
                        var dateString = string.Format("{0}年{1}月{2}日", party.DisplayStartDate.Year,
                            party.DisplayStartDate.Month, party.DisplayStartDate.Day);
                        var strtype = "礼遇日";
                        if (party.PartyType == 0)
                        {
                            strtype = "爱PARTY";
                        }
                        var msg = string.Format(coHostConsultantDeleteMessageFormat, party.AppliedName, dateString,
                            strtype);

                        MessageSender.Send(pc.ContactId, msg, smsType);

                    }
                }
            }
            partyOperation.DeleteByFunc(p => p.PartyKey == dto.PartyKey);
            return new PartyApplicationCommandResponse
            {
                PartyKey = party.PartyKey,
                EventKey = party.EventKey,
                CreateDateUtc = party.CreateDate
            };
        }

        public List<InvitationEntryWithDetail> GetOpenForInvitationDetail(Guid partyKey)
        {
            PartyEntity party = partyOperation.GetSingleDataByFunc(p => p.PartyKey == partyKey);
            var events = _eventsProvider.GetDetailBy(party.EventKey);
            var workShop = workshopProvider.GetWorkshopById(party.WorkshopId, party.EventKey);
            var statistics = _invitationRepository.GetInvitationStatistics(partyKey);
            List<InvitationEntryWithDetail> list = new List<InvitationEntryWithDetail>();
            foreach (EventEntryWithDetail ed in events)
            {
                list.Add(new InvitationEntryWithDetail
                {
                    AppliedContactId = party.AppliedContactID,
                    AppliedName = party.AppliedName,
                    EventKey = party.EventKey,
                    Category = ed.Category,
                    CoHostConsultants = GetCoHostConsutlants(partyKey),
                    InvitationToken = _partyReposity.GenerateToken(partyKey, party.AppliedContactID, party.WorkshopId),
                    IsSelfHost = party.OrganizationType == 0,
                    PartyEndDate = party.EndDate,
                    PartyStartDate = party.StartDate,
                    PartyDisplayEndDate = party.DisplayEndDate,
                    PartyDisplayStartDate = party.DisplayStartDate,
                    PartyKey = partyKey,
                    Title = ed.Title,
                    WorkshopLocation = workShop.Location,
                    WorkshopName = workShop.Name,
                    PartyInviteesCount = statistics.PartyInviteesCount,
                    SelfInviteesCount = statistics.SelfInviteesCount,
                    UnitInviteesCount = statistics.UnitInviteesCount,
                    PartySigninCount = statistics.PartySigninCount,
                    UnitInvitationSummary = statistics.UnitInvitationSummary,
                    SelfUnitID = _context.User.Unit
                });
            }
            return list;
        }

        public List<SigninEntryWithDetail> GetSigninDetail(Guid partyKey)
        {
            PartyEntity party = partyOperation.GetSingleDataByFunc(p => p.PartyKey == partyKey);
            var events = _eventsProvider.GetDetailBy(party.EventKey);
            var workShop = workshopProvider.GetWorkshopById(party.WorkshopId, party.EventKey);
            var statistics = _invitationRepository.GetSigninStatistics(partyKey);
            List<SigninEntryWithDetail> list = new List<SigninEntryWithDetail>();
            foreach (EventEntryWithDetail ed in events)
            {
                list.Add(new SigninEntryWithDetail
                {
                    EventKey = ed.EventKey,
                    AppliedContactId = party.AppliedContactID,
                    AppliedName = party.AppliedName,
                    Category = ed.Category,
                    CoHostConsultants = GetCoHostConsutlants(partyKey),
                    InvitationToken = _partyReposity.GenerateToken(partyKey, party.AppliedContactID, party.WorkshopId),
                    IsSelfHost = party.OrganizationType == 0,
                    PartyEndDate = party.EndDate,
                    PartyDisplayEndDate = party.DisplayEndDate,
                    PartyKey = partyKey,
                    PartyStartDate = party.StartDate,
                    PartyDisplayStartDate = party.DisplayStartDate,
                    Title = ed.Title,
                    WorkshopLocation = workShop.Location,
                    WorkshopName = workShop.Name,
                    PartyInviteesCount = statistics.PartyInviteesCount,
                    SelfInviteesCount = statistics.SelfInviteesCount,
                    UnitInviteesCount = statistics.UnitInviteesCount,
                    UnitInvitationSummary = statistics.UnitInvitationSummary,
                    PartySigninCount = statistics.PartySigninCount,
                    SelfUnitID = _context.User.Unit
                });
            }
            return list;
        }

        public List<FeedbackEntryWithDetail> GetFeedbackDetail(Guid partyKey)
        {
            PartyEntity party = partyOperation.GetSingleDataByFunc(p => p.PartyKey == partyKey);
            var events = _eventsProvider.GetDetailBy(party.EventKey);
            var workShop = workshopProvider.GetWorkshopById(party.WorkshopId, party.EventKey);
            var statistics = _invitationRepository.GetFeedbackStatistics(partyKey);
            List<FeedbackEntryWithDetail> list = new List<FeedbackEntryWithDetail>();
            foreach (EventEntryWithDetail ed in events)
            {
                list.Add(new FeedbackEntryWithDetail
                {
                    EventKey = ed.EventKey,
                    AppliedContactId = party.AppliedContactID,
                    AppliedName = party.AppliedName,
                    Category = ed.Category,
                    CoHostConsultants = GetCoHostConsutlants(partyKey),
                    InvitationToken = _partyReposity.GenerateToken(partyKey, party.AppliedContactID, party.WorkshopId),
                    IsSelfHost = party.OrganizationType == 0,
                    PartyEndDate = party.EndDate,
                    PartyDisplayEndDate = party.DisplayEndDate,
                    PartyKey = partyKey,
                    PartyStartDate = party.StartDate,
                    PartyDisplayStartDate = party.DisplayStartDate,
                    Title = ed.Title,
                    WorkshopLocation = workShop.Location,
                    WorkshopName = workShop.Name,
                    PartyInviteesCount = statistics.PartyInviteesCount,
                    SelfInviteesCount = statistics.SelfInviteesCount,
                    UnitInviteesCount = statistics.UnitInviteesCount,
                    UnitInvitationSummary = statistics.UnitInvitationSummary,
                    PartySigninCount = statistics.PartySigninCount,
                    SelfUnitID = _context.User.Unit,
                    NewComersCount = statistics.NewComersCount,
                    PromotionCount = statistics.PromotionCount,
                    OrderedCustomerCount = statistics.OrderedCustomerCount
                });
            }
            return list;
        }

        public List<InvitationEntry> GetPartyInvitation()
        {

            long hostContactID;
            var currentDir = _context.ConsultantContactId;
            if (_context.User.IsNotDirector())
            {
                var dir = _unitOperation.GetSingleDataByFunc(c => c.UnitID == _context.User.Unit);
                currentDir = dir.Director;
            }

            var unionpartyitem = partyOperation.Join<PartyEntity, UniteeEntity>((p, u) => p.PartyKey == u.PartyKey
                && u.UniteeContactID == currentDir &&
                    p.DisplayEndDate > DateTime.Now);
            if (unionpartyitem == null || unionpartyitem.Count == 0)
            {
                hostContactID = currentDir;
            }
            else
            {

                hostContactID = unionpartyitem.FirstOrDefault().AppliedContactID;
            }
            var invitations = partyOperation.Join<PartyEntity, EventEntity>(
                (p, e) => p.EventKey == e.EventKey &&
                    p.AppliedContactID == hostContactID &&
                    e.ApplicationEndDate < DateTime.Now &&
                    p.DisplayEndDate > DateTime.Now.Date.AddDays(1));
            var result = new List<InvitationEntry>();
            foreach (var item in invitations)
            {
                var party = partyOperation.GetSingleDataByFunc(p => p.PartyKey == item.PartyKey);
                var events = _eventsProvider.GetDetailBy(party.EventKey).FirstOrDefault();
                var workShop = workshopProvider.GetWorkshopById(party.WorkshopId, party.EventKey);
                var statistics = _invitationRepository.GetInvitationStatistics(item.PartyKey);
                result.Add(new InvitationEntry
                {
                    AppliedContactId = party.AppliedContactID,
                    AppliedName = party.AppliedName,
                    EventKey = events.EventKey,
                    Category = events.Category,
                    IsSelfHost = party.OrganizationType == 0,
                    PartyEndDate = party.EndDate,
                    PartyStartDate = party.StartDate,
                    PartyDisplayEndDate = party.DisplayEndDate,
                    PartyDisplayStartDate = party.DisplayStartDate,
                    PartyInviteesCount = statistics.PartyInviteesCount,
                    PartyKey = item.PartyKey,
                    SelfInviteesCount = statistics.SelfInviteesCount,
                    Title = events.Title,
                    UnitInviteesCount = statistics.UnitInviteesCount,
                    WorkshopLocation = workShop.Location,
                    WorkshopName = workShop.Name,
                    SelfUnitID = _context.User.Unit
                });
            }
            return result;
        }

        public object PartyInvitationInfo(PartyInvitation dto)
        {
            var token = _partyReposity.GetTokenEntity(dto.InvitationToken);
            var party = partyOperation.GetSingleDataByFunc(p => p.PartyKey == token.PartyKey.Value);
            if (party == null)
            {

                throw new HttpError(HttpStatusCode.BadRequest, "ResourceError", ConfigurationManager.AppSettings["PartyNotExistsError"]);
            }
            _logger.Info(string.Format("PartyInvitationInfo, {0}\t{1}\t{2}", dto.InvitationToken, token.CurrentUserContactID, token.UnitID));
            var partyDetail = BuildApplicationEntryDetail(party, false);
            long forworder = token.CurrentUserContactID ?? 0;
            var consultant = consultantOperation.GetSingleDataByFunc(c => c.ContactID == forworder);
            return new PartyInvitationResponse
            {
                PartyKey = partyDetail.PartyKey,
                EventTitle = partyDetail.Title,
                ForwordContactID = consultant.ContactID,
                ForwordName = string.Format("{0}{1}", consultant.LastName, consultant.FirstName),
                PartyEndDate = partyDetail.PartyEndDate,
                PartyStartDate = partyDetail.PartyStartDate,
                PartyDisplayEndDate = partyDetail.PartyDisplayEndDate,
                PartyDisplayStartDate = partyDetail.PartyDisplayStartDate,
                ApplicationStartDate = partyDetail.ApplicationStartDate,
                ApplicationEndDate = partyDetail.ApplicationEndDate,
                WorkshopLocation = partyDetail.WorkshopLocation
            };
        }
    }
}
