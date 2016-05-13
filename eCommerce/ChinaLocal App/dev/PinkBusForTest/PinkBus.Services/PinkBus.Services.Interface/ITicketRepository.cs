using PinkBus.Services.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PinkBus.Services.Interface
{
    public interface ITicketRepository
    {
        QueryTicketsResponse QueryTickets(QueryTickets dto);
        GetTicketDetailResponse GetTicketDetail(GetTicketDetail dto);
        ApplyTicketResponse ApplyTicket(ApplyTicket dto);
        QueryApplyTicketResultResponse QueryApplyTicketResult(QueryApplyTicketResult dto);
        BestowalTicketResponse BestowalTicket(BestowalTicket dto);

        CreateInvitationResponse CreateInvitation(CreateInvitation dto);
        CancelInvitationResponse CancelInvitation(CancelInvitation dto);
       
    }
}
