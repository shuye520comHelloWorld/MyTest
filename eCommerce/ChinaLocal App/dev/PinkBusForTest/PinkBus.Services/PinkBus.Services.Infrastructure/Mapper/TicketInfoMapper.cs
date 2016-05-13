
using PinkBus.Services.Contract;
using PinkBus.Services.Entity;
using PinkBus.Services.Infrastructure.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using contract = PinkBus.Services.Contract;

namespace PinkBus.Services.Infrastructure.Mapper
{
    public static class TicketInfoMapper
    {
        public static List<contract.Ticket> TicketToResponse(this List<TicketInfo> ticketList)
        {
            List<contract.Ticket> list = new List<contract.Ticket>();
            ticketList.ForEach((each) =>
            {
                contract.Ticket ticket = new contract.Ticket
                {
                    TicketKey = each.TicketKey,
                    TicketStatus = (TicketStatus)each.TicketStatus,
                    TicketType = (TicketType)each.TicketType,
                    CustomerName = each.CustomerName,
                    CustomerPhone = each.CustomerPhone,
                    CustomerKey=each.CustomerKey
                };
                list.Add(ticket);
            });

            return list;
        }

        public static contract.GetTicketDetailResponse TicketDetailInfoToResponse(this TicketDetailInfo ticketInfo)
        {
            contract.GetTicketDetailResponse response = new contract.GetTicketDetailResponse
            {
                ConsultantPhone = ticketInfo.PhoneNumber,
                FirstName = ticketInfo.FirstName,
                LastName = ticketInfo.LastName,
                TicketStatus = (TicketStatus)ticketInfo.TicketStatus,
                SMSToken = ticketInfo.SMSToken,
                TicketType = (TicketType)ticketInfo.TicketType,
                CustomerKey = ticketInfo.CustomerKey,
                CustomerName = ticketInfo.CustomerName,
                CustomerPhone = ticketInfo.CustomerPhone,
                TicketKey = ticketInfo.TicketKey,               
                SessionStartDate=ticketInfo.SessionStartDate,
                SessionEndDate = ticketInfo.SessionEndDate
            };
            return response;
        }
    }
}
