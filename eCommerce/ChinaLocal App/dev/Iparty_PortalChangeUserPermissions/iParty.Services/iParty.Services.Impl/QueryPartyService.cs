using iParty.Services.Contract;
using iParty.Services.Entity;
using iParty.Services.Interface;
using NLog;
using QuartES.Services.Core;
using ServiceStack;
using System;
using System.Collections.Generic;
using System.Linq;

namespace iParty.Services.Impl
{
    public sealed class QueryPartyService : Service
    {
        readonly IContext _context;
        readonly IQueryCriteria _queryCriteria;
        readonly IPartyQuery _partyQuery;
        readonly IEventsProvider _eventsProvider;
        readonly IWorkshopsProvider _workshopsProvider;
        readonly Logger _logger = LogManager.GetCurrentClassLogger();

        public QueryPartyService(
            IContext context,
            IQueryCriteria queryCriteria,
            IPartyQuery partyQuery,
            IEventsProvider eventsProvider,
            IWorkshopsProvider workshopsProvider)
        {
            _context = context;
            _queryCriteria = queryCriteria;
            _partyQuery = partyQuery;
            _eventsProvider = eventsProvider;
            _workshopsProvider = workshopsProvider;
        }

        public object Get(QueryParty dto)
        {
            long total = 0;
            List<object> partyList = new List<object>();
            var isFinished = dto.IsFinished.HasValue ? dto.IsFinished.Value : false;

            if (_context.User.IsDirector())
            {
                if (isFinished)
                {
                    var isDetail = dto.PartyKey.HasValue ? true : false;
                    if (isDetail)
                    {
                        //partyList = _partyQuery.GetHistory(dto.PartyKey.Value);
                        //total = partyList.Count;

                        AppendSigninDetailTo(partyList, dto.PartyKey.Value);

                    }
                    else
                    {
                        //switch (dto.PartyStage.Value)
                        //{
                        //    case PartyStage.Finished:
                                AppendHistoryApplicationQueryTo(partyList);
                        //        break;
                        //    default:
                        //        break;

                       // }
                    }
                }
                else
                {
                    var isDetail = dto.EventKey.HasValue || dto.PartyKey.HasValue ? true : false;
                    if (isDetail)
                    {
                        if (dto.EventKey.HasValue)
                        {
                            AppendEventDetailTo(partyList, dto.EventKey.Value);
                        }
                        else
                        {
                            switch (dto.PartyStage.Value)
                            {
                                case PartyStage.Created:
                                    AppendApplicationDetailTo(partyList, dto.PartyKey.Value);
                                    break;
                                case PartyStage.OpenForInvitation:
                                    AppendInvitationDetailTo(partyList, dto.PartyKey.Value);
                                    break;
                                case PartyStage.OpenForInviteeSignIn:
                                    AppendSigninDetailTo(partyList, dto.PartyKey.Value);
                                    break;
                                default:
                                    break;
                            }
                        }
                    }
                    else
                    {
                        AppendEventQueryTo(partyList);
                        AppendApplicationQueryTo(partyList);
                        AppendInvitationQueryTo(partyList);
                        AppendSigninQueryTo(partyList);
                    }
                    total = partyList.Count;
                }
            }
            else if (_context.User.IsNotDirector())
            {
                var isDetail = dto.PartyKey.HasValue ? true : false;
                if (isFinished)
                {
                    if (isDetail)
                    {
                        AppendSigninDetailTo(partyList, dto.PartyKey.Value);
                    }
                    else
                    {
                        AppendHistoryApplicationQueryTo(partyList);
                    }
                }
                else
                {
                    if (isDetail)
                    {
                        switch (dto.PartyStage.Value)
                        {
                            case PartyStage.OpenForInvitation:
                                AppendInvitationDetailTo(partyList, dto.PartyKey.Value);
                                break;
                            case PartyStage.OpenForInviteeSignIn:
                                AppendSigninDetailTo(partyList, dto.PartyKey.Value);
                                break;
                            default:
                                break;
                        }
                    }
                    else
                    {
                        AppendInvitationQueryTo(partyList);
                        AppendSigninQueryTo(partyList);
                    }
                    total = partyList.Count;
                }
            }

            return new QueryPartyResponse()
            {
                Results = partyList,
                _Meta = new Meta { Total = total, Limit = _queryCriteria.Limit, Offset = _queryCriteria.Offset }
            };
        }

        private void AppendSigninQueryTo(List<object> partyList)
        {
            var signins = _partyQuery.GetSignin();
            if (signins == null)
            {
                return;
            }
            foreach (var signin in signins)
            {
                partyList.Add(new PartyrSigninQueryResult
                {
                    EventKey = signin.EventKey,
                    AppliedContactId = signin.AppliedContactId,
                    AppliedName = signin.AppliedName,
                    PartyKey = signin.PartyKey,
                    Title = signin.Title,
                    WorkshopName = signin.WorkshopName,
                    WorkshopLocation = signin.WorkshopLocation,
                    Category = signin.Category,
                    Stage = PartyStage.OpenForInviteeSignIn,
                    PartyStartDate = signin.PartyStartDate,
                    PartyEndDate = signin.PartyEndDate,
                    PartyInviteesCount = signin.PartyInviteesCount,
                    PartySigninCount = signin.PartySigninCount,
                    SelfUnitID = signin.SelfUnitID,
                    UnitInviteesCount = signin.UnitInviteesCount
                });
            }
        }

        private void AppendSigninDetailTo(List<object> partyList, Guid partyKey)
        {
            var signins = _partyQuery.GetSigninDetail(partyKey);
            foreach (var signin in signins)
            {
                partyList.Add(new PartyrSigninQueryResult
                {
                    EventKey = signin.EventKey,
                    AppliedName = signin.AppliedName,
                    AppliedContactId = signin.AppliedContactId,
                    PartyKey = signin.PartyKey,
                    Title = signin.Title,
                    WorkshopName = signin.WorkshopName,
                    WorkshopLocation = signin.WorkshopLocation,
                    Category = signin.Category,
                    Stage = PartyStage.OpenForInviteeSignIn,
                    PartyStartDate = signin.PartyStartDate,
                    PartyEndDate = signin.PartyEndDate,
                    PartyInviteesCount = signin.PartyInviteesCount,
                    PartySigninCount = signin.PartySigninCount,
                    IsSelfHost = signin.IsSelfHost,
                    UnitInviteesCount = signin.UnitInviteesCount,
                    SelfInviteesCount = signin.SelfInviteesCount,
                    CoHostConsultants = signin.CoHostConsultants,
                    UnitInvitationSummary = signin.UnitInvitationSummary,
                    InvitationToken = signin.InvitationToken,
                    SelfUnitID = signin.SelfUnitID
                });
            }
        }

        private void AppendInvitationQueryTo(List<object> partyList)
        {
            var invitations = _partyQuery.GetInvitation();
            if (invitations == null)
            {
                return;
            }
            foreach (var invitation in invitations)
            {
                partyList.Add(new PartyrInvitationQueryResult
                {
                    EventKey = invitation.EventKey,
                    PartyKey = invitation.PartyKey,
                    Title = invitation.Title,
                    WorkshopName = invitation.WorkshopName,
                    WorkshopLocation = invitation.WorkshopLocation,
                    Category = invitation.Category,
                    Stage = PartyStage.OpenForInvitation,
                    PartyStartDate = invitation.PartyStartDate,
                    PartyEndDate = invitation.PartyEndDate,
                    IsSelfHost = invitation.IsSelfHost,
                    PartyInviteesCount = invitation.PartyInviteesCount,
                    UnitInviteesCount = invitation.IsSelfHost ? 0 : invitation.UnitInviteesCount,
                    SelfInviteesCount = invitation.SelfInviteesCount,
                    AppliedContactId = invitation.AppliedContactId,
                    AppliedName = invitation.AppliedName,
                    SelfUnitID = invitation.SelfUnitID
                });
            }
        }

        private void AppendInvitationDetailTo(List<object> partyList, Guid partyKey)
        {
            var invitations = _partyQuery.GetInvitationDetail(partyKey);
            foreach (var invitation in invitations)
            {
                partyList.Add(new PartyrInvitationQueryResult
                {
                    EventKey = invitation.EventKey,
                    PartyKey = invitation.PartyKey,
                    Title = invitation.Title,
                    WorkshopName = invitation.WorkshopName,
                    WorkshopLocation = invitation.WorkshopLocation,
                    Category = invitation.Category,
                    Stage = PartyStage.OpenForInvitation,
                    PartyStartDate = invitation.PartyStartDate,
                    PartyEndDate = invitation.PartyEndDate,
                    IsSelfHost = invitation.IsSelfHost,
                    PartyInviteesCount = invitation.PartyInviteesCount,
                    UnitInviteesCount = invitation.UnitInviteesCount,
                    SelfInviteesCount = invitation.SelfInviteesCount,
                    CoHostConsultants = invitation.CoHostConsultants,
                    UnitInvitationSummary = invitation.UnitInvitationSummary,
                    InvitationToken = invitation.InvitationToken,
                    AppliedContactId = invitation.AppliedContactId,
                    AppliedName = invitation.AppliedName,
                    SelfUnitID = invitation.SelfUnitID
                });
            }
        }

        /// <summary>
        /// query all all applications which is not in invation stage
        /// </summary>
        /// <param name="partyList"></param>
        private void AppendApplicationQueryTo(List<object> partyList)
        {
            var applications = _partyQuery.GetApplication();

            foreach (var application in applications)
            {
                if (application.isApplicationEnd)
                {
                    continue;
                }
                partyList.Add(new PartyCreatedQueryResult
                {
                    EventKey = application.EventKey,
                    PartyKey = application.PartyKey,
                    AppliedContactId = application.AppliedContactId,
                    AppliedName = application.AppliedName,
                    IsSelfHost = application.IsSelfHost,
                    Title = application.Title,
                    isOwner = application.isOwner,
                    WorkShopId = application.WorkShopId,
                    WorkshopName = application.WorkshopName,
                    WorkshopLocation = application.WorkshopLocation,
                    Category = application.Category,
                    Stage = PartyStage.Created,
                    PartyStartDate = application.PartyStartDate,
                    PartyEndDate = application.PartyEndDate
                });
            }
        }


        /// <summary>
        /// query  applications  which is finished
        /// </summary>
        /// <param name="partyList"></param>
        private void AppendHistoryApplicationQueryTo(List<object> partyList)
        {
            var applications = _partyQuery.GetHistory();

            foreach (var application in applications)
            {
               
                partyList.Add(new PartyCreatedQueryResult
                {
                    EventKey = application.EventKey,
                    PartyKey = application.PartyKey,
                    AppliedContactId = application.AppliedContactId,
                    AppliedName = application.AppliedName,
                    IsSelfHost = application.IsSelfHost,
                    Title = application.Title,
                    isOwner = application.isOwner,
                    WorkShopId = application.WorkShopId,
                    WorkshopName = application.WorkshopName,
                    WorkshopLocation = application.WorkshopLocation,
                    Category = application.Category,
                    Stage = PartyStage.Created,
                    PartyStartDate = application.PartyStartDate,
                    PartyEndDate = application.PartyEndDate,
                    InvitationCount=application.InvitationCount,
                    CheckInCount=application.CheckInCount
                });
            }
        }


        private void AppendApplicationDetailTo(List<object> partyList, Guid partyKey)
        {
            var applications = _partyQuery.GetApplicationDetail(partyKey);

            foreach (var application in applications)
            {
                if (application.isApplicationEnd)
                {
                    continue;
                }
                var workshops = _workshopsProvider.GetWorkshops(application.EventKey);
                partyList.Add(new PartyCreatedQueryResult
                {

                    EventKey = application.EventKey,
                    PartyKey = application.PartyKey,
                    IsSelfHost = application.IsSelfHost,
                    AppliedContactId = application.AppliedContactId,
                    AppliedName = application.AppliedName,
                    Title = application.Title,
                    isOwner = application.isOwner,
                    WorkShopId = application.WorkShopId,
                    WorkshopName = application.WorkshopName,
                    WorkshopLocation = application.WorkshopLocation,
                    Category = application.Category,
                    Stage = PartyStage.Created,
                    PartyStartDate = application.PartyStartDate,
                    PartyEndDate = application.PartyEndDate,
                    Description = application.Description,
                    Notes = application.Notes,
                    Workshops = workshops,
                    ApplicationStartDate = application.ApplicationStartDate,
                    ApplicationEndDate = application.ApplicationEndDate,
                    PartyAllowEndDate = application.PartyAllowEndDate,
                    PartyAllowStartDate = application.PartyAllowStartDate,
                    EventStartDate = application.EventStartDate,
                    EventEndDate = application.EventEndDate,
                    CoHostConsultants = application.CoHostConsultants
                });
            }
        }


        private void AppendHistoryApplicationDetailTo(List<object> partyList, Guid partyKey)
        {
            var applications = _partyQuery.GetApplicationDetail(partyKey);

            foreach (var application in applications)
            {
               
                var workshops = _workshopsProvider.GetWorkshops(application.EventKey);
                partyList.Add(new PartyCreatedQueryResult
                {

                    EventKey = application.EventKey,
                    PartyKey = application.PartyKey,
                    IsSelfHost = application.IsSelfHost,
                    AppliedContactId = application.AppliedContactId,
                    AppliedName = application.AppliedName,
                    Title = application.Title,
                    isOwner = application.isOwner,
                    WorkShopId = application.WorkShopId,
                    WorkshopName = application.WorkshopName,
                    WorkshopLocation = application.WorkshopLocation,
                    Category = application.Category,
                    Stage = PartyStage.Created,
                    PartyStartDate = application.PartyStartDate,
                    PartyEndDate = application.PartyEndDate,
                    Description = application.Description,
                    Notes = application.Notes,
                    Workshops = workshops,
                    ApplicationStartDate = application.ApplicationStartDate,
                    ApplicationEndDate = application.ApplicationEndDate,
                    PartyAllowEndDate = application.PartyAllowEndDate,
                    PartyAllowStartDate = application.PartyAllowStartDate,
                    EventStartDate = application.EventStartDate,
                    EventEndDate = application.EventEndDate,
                    CoHostConsultants = application.CoHostConsultants
                });
            }
        }

        private void AppendEventQueryTo(List<object> partyList)
        {
            var events = _eventsProvider.GetBy();
            var applications = _partyQuery.GetApplication();
            var appliedEventList = new List<Guid>();
            foreach (var item in applications)
            {
                appliedEventList.Add(item.EventKey);
            }
            foreach (var anEvent in events)
            {
                if (appliedEventList.Contains(anEvent.EventKey) || anEvent.ApplicationEndDate < DateTime.Now)
                {
                    continue;
                }
                PartyCategory category;
                Enum.TryParse<PartyCategory>(anEvent.Category.ToString(), out category);
                partyList.Add(new ApplicationOpenedQueryResult
                {
                    EventKey = anEvent.EventKey,
                    Title = anEvent.Title,
                    Category = category,
                    Stage = PartyStage.OpenForApplication,
                    ApplicationStartDate = anEvent.ApplicationStartDate,
                    ApplicationEndDate = anEvent.ApplicationEndDate

                });
            }

            //_eventsProvider.GetNotAppliedEvents(partyList);
        }

        private void AppendEventDetailTo(List<object> partyList, Guid eventKey)
        {
            var events = _eventsProvider.GetDetailBy(eventKey);

            foreach (var anEvent in events)
            {
                var workshops = _workshopsProvider.GetWorkshops(anEvent.EventKey);
                partyList.Add(new ApplicationOpenedQueryResult
                {
                    EventKey = anEvent.EventKey,
                    Title = anEvent.Title,
                    Category = anEvent.Category,
                    Stage = PartyStage.OpenForApplication,
                    ApplicationStartDate = anEvent.ApplicationStartDate,
                    ApplicationEndDate = anEvent.ApplicationEndDate,
                    Description = anEvent.Description,
                    Notes = anEvent.Notes,
                    Workshops = workshops,
                    EventStartDate = anEvent.EventStartDate,
                    EventEndDate = anEvent.EventEndDate,
                    PartyAllowEndDate = anEvent.PartyAllowEndDate,
                    PartyAllowStartDate = anEvent.PartyAllowStartDate
                });
            }
        }
    }
}
