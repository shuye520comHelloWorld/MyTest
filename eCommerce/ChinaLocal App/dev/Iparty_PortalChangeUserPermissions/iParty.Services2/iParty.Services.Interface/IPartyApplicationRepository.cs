
using iParty.Services.Contract;
using iParty.Services.Entity;
using System;
using System.Collections.Generic;
namespace iParty.Services.Interface
{
    public interface IPartyApplicationRepository
    {
        object Create(CreatePartyApplication dto);
        object Update(UpdatePartyApplication dto);
        object Delete(DeletePartyApplication dto);
        object PartyInvitationInfo(PartyInvitation dto);
        IEnumerable<Consultant> GetCoHostConsutlants(Guid partyKey);
        List<ApplicationEntry> Get();
        List<ApplicationEntry> GetHistory();
       // List<ApplicationEntryWithDetail> GetHistoryDetail();
        List<ApplicationEntryWithDetail> GetDetail(Guid PartyKey);
        List<InvitationEntryWithDetail> GetOpenForInvitationDetail(Guid partyKey);
        List<SigninEntryWithDetail> GetSigninDetail(Guid partyKey);
        List<FeedbackEntryWithDetail> GetFeedbackDetail(Guid partyKey);
        List<InvitationEntry> GetPartyInvitation();
    }
}
