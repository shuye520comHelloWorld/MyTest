using iParty.Services.Contract;
using iParty.Services.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iParty.Services.Interface
{
    public interface IInvitationRepository
    {
        object CheckCustomer(CheckCustomerInvitation req);
        object Accept(AcceptInvitation req);
        object Cancel(CancelInvitation req);
        object CheckIn(InvitationCheckIn req);
        CustomerDetailResponse GetCustomerDetail(CustomerDetail dto);
        IEnumerable<QueryCustomerResponse> GetCustomerList(QueryCustomer dto);
        InvitationStatistics GetInvitationStatistics(Guid partyKey);
        SigninStatistics GetSigninStatistics(Guid partyKey);
        FeedbackStatistics GetFeedbackStatistics(Guid partyKey);
        List<SigninEntry> GetSignin();
    }

    public class InvitationStatistics
    {
        public Guid PartyKey { get; set; }
        public int UnitInviteesCount { get; set; }
        public int SelfInviteesCount { get; set; }
        public int PartyInviteesCount { get; set; }
        public IEnumerable<UnitInvitationSummary> UnitInvitationSummary { get; set; }
        public int PartySigninCount { get; set; } // there maybe checkin in invitation period
    }

    public class SigninStatistics : InvitationStatistics
    {
        
    }

    public class FeedbackStatistics : SigninStatistics
    {
        public int NewComersCount { get; set; }
        public int PromotionCount { get; set; }
        public int OrderedCustomerCount { get; set; }
    }
}
