using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iParty.Services.Entity
{
    /// Notice that the query result varies
    /// based on filter options and target party's current stage

    public class ApplicationOpenedQueryResult
    {
        // IsDetail = false 
        public Guid? EventKey { get; set; }
        public Guid? PartyKey { get; set; }
        public string Title { get; set; }
        public PartyCategory Category { get; set; }
        public PartyStage Stage { get; set; }
        public DateTime ApplicationStartDate { get; set; }
        public DateTime ApplicationEndDate { get; set; }
        public DateTime? FeedbackEndDate { get; set; }

        // IsDetail = true
        public string Description { get; set; }
        public IEnumerable<string> Notes { get; set; }
        public IEnumerable<Workshop> Workshops { get; set; }
        public DateTime? EventStartDate { get; set; }
        public DateTime? EventEndDate { get; set; }
        public DateTime PartyAllowEndDate { get; set; }
        public DateTime PartyAllowStartDate { get; set; }
    }

    public class PartyCreatedQueryResult
    {
        // IsDetail = false 
        public Guid EventKey { get; set; }
        public Guid? PartyKey { get; set; }
        public bool? IsSelfHost { get; set; }
        public string Title { get; set; }
        public string AppliedName { get; set; }
        public long AppliedContactId { get; set; }
        public int WorkShopId { get; set; }
        public string WorkshopName { get; set; }
        public string WorkshopLocation { get; set; }
        public PartyCategory Category { get; set; }
        public PartyStage Stage { get; set; }
        public DateTime? PartyStartDate { get; set; }
        public DateTime? PartyEndDate { get; set; }
        public DateTime? PartyDisplayStartDate { get; set; }
        public DateTime? PartyDisplayEndDate { get; set; }
        public bool isOwner { get; set; }
        // IsDetail = true
        public string Description { get; set; }
        public IEnumerable<string> Notes { get; set; }
        public IEnumerable<Workshop> Workshops { get; set; }
        public DateTime? ApplicationStartDate { get; set; }
        public DateTime? ApplicationEndDate { get; set; }
        public DateTime? EventStartDate { get; set; }
        public DateTime? EventEndDate { get; set; }
        public DateTime PartyAllowEndDate { get; set; }
        public DateTime PartyAllowStartDate { get; set; }
        public long? InvitationCount { get; set; }
        public long? CheckInCount { get; set; }
        public IEnumerable<Consultant> CoHostConsultants { get; set; }
    }

    public class PartyrInvitationQueryResult
    {
        // IsDetail = false 
        public Guid EventKey { get; set; }
        public Guid? PartyKey { get; set; }
        public string Title { get; set; }
        public string WorkshopName { get; set; }
        public string WorkshopLocation { get; set; }
        public string AppliedName { get; set; }
        public long AppliedContactId { get; set; }
        public PartyCategory Category { get; set; }
        public PartyStage Stage { get; set; }
        public DateTime? PartyStartDate { get; set; }
        public DateTime? PartyEndDate { get; set; }
        public DateTime? PartyDisplayStartDate { get; set; }
        public DateTime? PartyDisplayEndDate { get; set; }
        public bool? IsSelfHost { get; set; }
        public int? PartyInviteesCount { get; set; }
        public int? UnitInviteesCount { get; set; }
        public int? SelfInviteesCount { get; set; }
        public int? PartySigninCount { get; set; }
        public string SelfUnitID { get; set; }

        // IsDetail = true
        public IEnumerable<Consultant> CoHostConsultants { get; set; }
        public IEnumerable<UnitInvitationSummary> UnitInvitationSummary { get; set; }
        // an encrypted string composed from PartyKey, ConsultantContactId, UserRole, WorkShopKey, UnitId
        public string InvitationToken { get; set; }
    }

    public class PartyrSigninQueryResult
    {
        // IsDetail = false 
        public Guid EventKey { get; set; }
        public Guid? PartyKey { get; set; }
        public string Title { get; set; }
        public string WorkshopName { get; set; }
        public string WorkshopLocation { get; set; }
        public string AppliedName { get; set; }
        public long AppliedContactId { get; set; }
        public PartyCategory Category { get; set; }
        public PartyStage Stage { get; set; }
        public DateTime? PartyStartDate { get; set; }
        public DateTime? PartyEndDate { get; set; }
        public DateTime? PartyDisplayStartDate { get; set; }
        public DateTime? PartyDisplayEndDate { get; set; }
        public int? PartyInviteesCount { get; set; }
        public int? PartySigninCount { get; set; }
        public string SelfUnitID { get; set; }
        // IsDetail = true
        public bool? IsSelfHost { get; set; }
        public int? UnitInviteesCount { get; set; }
        public int? SelfInviteesCount { get; set; }
        public IEnumerable<Consultant> CoHostConsultants { get; set; }
        public IEnumerable<UnitInvitationSummary> UnitInvitationSummary { get; set; }
        // an encrypted string composed from PartyKey, ConsultantContactId, UserRole, WorkShopKey, UnitId
        public string InvitationToken { get; set; }
    }


    public class PartyrFeedbackQueryResult
    {
        // IsDetail = false 
        public Guid EventKey { get; set; }
        public Guid? PartyKey { get; set; }
        public string Title { get; set; }
        public string WorkshopName { get; set; }
        public string WorkshopLocation { get; set; }
        public string AppliedName { get; set; }
        public long AppliedContactId { get; set; }
        public PartyCategory Category { get; set; }
        public PartyStage Stage { get; set; }
        public DateTime? PartyStartDate { get; set; }
        public DateTime? PartyEndDate { get; set; }
        public DateTime? PartyDisplayStartDate { get; set; }
        public DateTime? PartyDisplayEndDate { get; set; }
        public int? PartyInviteesCount { get; set; }
        public int? PartySigninCount { get; set; }
        public string SelfUnitID { get; set; }
        // IsDetail = true
        public bool? IsSelfHost { get; set; }
        public int? UnitInviteesCount { get; set; }
        public int? SelfInviteesCount { get; set; }
        public IEnumerable<Consultant> CoHostConsultants { get; set; }
        public IEnumerable<UnitInvitationSummary> UnitInvitationSummary { get; set; }
        // an encrypted string composed from PartyKey, ConsultantContactId, UserRole, WorkShopKey, UnitId
        public string InvitationToken { get; set; }
        public int NewComersCount { get; set; }
        public int PromotionCount { get; set; }
        public int OrderedCustomerCount { get; set; }
    }

}
