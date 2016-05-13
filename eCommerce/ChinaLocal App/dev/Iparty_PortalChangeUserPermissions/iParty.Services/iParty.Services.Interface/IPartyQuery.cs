using iParty.Services.Entity;
using QuartES.Services.Core;
using System;
using System.Collections.Generic;

namespace iParty.Services.Interface
{
    public interface IPartyQuery
    {
        List<ApplicationEntry> GetHistory();
        //List<ApplicationEntryWithDetail> GetHistoryDetail(Guid partyKey);

        List<ApplicationEntry> GetApplication();
        List<ApplicationEntryWithDetail> GetApplicationDetail(Guid partyKey);

        List<InvitationEntry> GetInvitation();
        List<InvitationEntryWithDetail> GetInvitationDetail(Guid partyKey);

        List<SigninEntry> GetSignin();
        List<SigninEntryWithDetail> GetSigninDetail(Guid partyKey);
    }

    public class ApplicationEntry
    {
        public Guid EventKey { get; set; }
        public Guid PartyKey { get; set; }
        public bool IsSelfHost { get; set; }
        public bool isOwner { get; set; }
        public string AppliedName { get; set; }
        public long AppliedContactId { get; set; }
        public string Title { get; set; }
        public int WorkShopId { get; set; }
        public string WorkshopName { get; set; }
        public string WorkshopLocation { get; set; }
        public bool isApplicationEnd { get; set; }
        public long? InvitationCount { get; set; }
        public long? CheckInCount { get; set; }

        public PartyCategory Category { get; set; }
        public PartyType PartyType { get; set; }
        public DateTime PartyStartDate { get; set; }
        public DateTime PartyEndDate { get; set; }
       
    }

    public class ApplicationEntryWithDetail : ApplicationEntry
    {
        public string Description { get; set; }
        public IEnumerable<string> Notes { get; set; }
        public DateTime ApplicationStartDate { get; set; }
        public DateTime ApplicationEndDate { get; set; }
        public DateTime EventStartDate { get; set; }
        public DateTime EventEndDate { get; set; }
        public DateTime PartyAllowEndDate { get; set; }
        public DateTime PartyAllowStartDate { get; set; }
        public IEnumerable<Consultant> CoHostConsultants { get; set; }
    }

    public class InvitationEntry
    {
        public Guid EventKey { get; set; }
        public Guid PartyKey { get; set; }
        public string Title { get; set; }
        public string WorkshopName { get; set; }
        public string WorkshopLocation { get; set; }
        public string AppliedName { get; set; }
        public long AppliedContactId { get; set; }
        public PartyCategory Category { get; set; }
        public DateTime PartyStartDate { get; set; }
        public DateTime PartyEndDate { get; set; }
        public bool IsSelfHost { get; set; }        
        public int PartyInviteesCount { get; set; }
        public int? UnitInviteesCount { get; set; }
        public int SelfInviteesCount { get; set; }
        public string SelfUnitID { get; set; }
    }

    public class InvitationEntryWithDetail : InvitationEntry
    {
        public IEnumerable<UnitInvitationSummary> UnitInvitationSummary { get; set; }
        public IEnumerable<Consultant> CoHostConsultants { get; set; }
        public string InvitationToken { get; set; }
    }

    public class SigninEntry
    {
        public Guid EventKey { get; set; }
        public Guid PartyKey { get; set; }
        public string Title { get; set; }
        public string WorkshopName { get; set; }
        public string WorkshopLocation { get; set; }
        public string AppliedName { get; set; }
        public long AppliedContactId { get; set; }
        public PartyCategory Category { get; set; }
        public DateTime PartyStartDate { get; set; }
        public DateTime PartyEndDate { get; set; }
        public int PartyInviteesCount { get; set; }
        public int PartySigninCount { get; set; }
        public string SelfUnitID { get; set; }
        public int UnitInviteesCount { get; set; }
    }

    public class SigninEntryWithDetail : SigninEntry
    {
        public bool IsSelfHost { get; set; }        
        public int SelfInviteesCount { get; set; }
        public IEnumerable<Consultant> CoHostConsultants { get; set; }
        public IEnumerable<UnitInvitationSummary> UnitInvitationSummary { get; set; }
        public string InvitationToken { get; set; }
    }

    public class PartyHistory
    {

    }
}