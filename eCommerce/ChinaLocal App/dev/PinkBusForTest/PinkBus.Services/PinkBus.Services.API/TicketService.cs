using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PinkBus.Services.Interface;
using PinkBus.Services.Contract;
using PinkBus.Services.OAuth.Attribute;
namespace PinkBus.Services.API
{
    [OAuthRequest(ProjectType.Intouch)]
    public class TicketService : ServiceStack.Service
    {
        private ITicketRepository ticketRepository;

        public TicketService(ITicketRepository ticketRepository)
        {
            this.ticketRepository = ticketRepository;
        }

        public ApplyTicketResponse Post(ApplyTicket dto)
        {
            return ticketRepository.ApplyTicket(dto);
        }
        public ApplyTicketResponse Put(ApplyTicket dto)
        {
            return ticketRepository.ApplyTicket(dto);
        }
        public QueryApplyTicketResultResponse Get(QueryApplyTicketResult dto)
        {
            return ticketRepository.QueryApplyTicketResult(dto);
        }

        public GetTicketDetailResponse Get(GetTicketDetail dto)
        {
            return ticketRepository.GetTicketDetail(dto);
        }

        public QueryTicketsResponse Get(QueryTickets dto)
        {
            return ticketRepository.QueryTickets(dto);
        }

        public BestowalTicketResponse Put(BestowalTicket dto)
        {
            return ticketRepository.BestowalTicket(dto);
        }

        public BestowalTicketResponse Post(BestowalTicket dto)
        {
            return ticketRepository.BestowalTicket(dto);
        }

        public CreateInvitationResponse Post(CreateInvitation dto)
        {
            return ticketRepository.CreateInvitation(dto);
        }

        public CreateInvitationResponse Put(CreateInvitation dto)
        {
            return ticketRepository.CreateInvitation(dto);
        }

        public CancelInvitationResponse Delete(CancelInvitation dto)
        {
            return ticketRepository.CancelInvitation(dto);
        }
        public CancelInvitationResponse Post(CancelInvitation dto)
        {
            return ticketRepository.CancelInvitation(dto);
        }

    }
}
