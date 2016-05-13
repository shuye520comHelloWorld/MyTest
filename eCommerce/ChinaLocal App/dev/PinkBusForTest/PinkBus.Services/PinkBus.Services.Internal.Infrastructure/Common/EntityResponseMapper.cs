using contract = PinkBus.Services.Internal.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PinkBus.Services.Internal.Contract;


namespace PinkBus.Services.Internal.Infrastructure.Common
{
    public static class EntityResponseMapper
    {
        public static List<contract.Ticket> TicketInfoToResponse(this List<TicketInfo> info)
        {
            List<contract.Ticket> list = new List<contract.Ticket>();
            info.ForEach(p =>
            {
                Contract.Ticket ticket = new contract.Ticket
                {
                    CheckinDate = p.CheckinDate,
                    ConsultantName = p.ConsultantName,
                    ConsultantPhoneNumber = p.ConsultantPhoneNumber,
                    ContactId = p.ContactId,
                    EventLocation = p.EventLocation,
                    EventTitle = p.EventTitle,
                    SessionEndDate = p.SessionEndDate,
                    SessionStartDate = p.SessionStartDate,
                    SMSToken = p.SMSToken,
                    TicketKey = p.TicketKey,
                    TicketStatus = ConvertPinkBusTicketStatus(p.TicketStatus, p.EventEndDate),
                    TicketType = (TicketType)p.TicketType,
                    CreatedDate = p.CreatedDate

                };

                list.Add(ticket);
            });

            return list;
        }

        /// <summary>
        ///  Created:0,Inviting:1,Invited:2,Bestowed:3，Canceled:4,Checkin:5,UnCheckin:6,Expired:7
        ///  customer ticket status must be in Invited,Canceled,Checkin,UnCheckin
        /// </summary>
        /// <param name="ticketStatus"></param>
        /// <returns></returns>
        private static TicketStatus ConvertPinkBusTicketStatus(short ticketStatus, DateTime EventEndDate)
        {
            switch (ticketStatus)
            {
                case 2:
                    DateTime currentDate = DateTime.Now;
                    if (currentDate > EventEndDate.Date.AddDays(1))
                        return TicketStatus.Invalid;
                    else
                        return TicketStatus.UnUsed;

                case 4:
                    return TicketStatus.Invalid;
            }
            return TicketStatus.Invalid;
        }

        public static List<contract.Customer> CustomerInfoToResponse(this List<CustomerInfo> info)
        {
            List<contract.Customer> list = new List<contract.Customer>();
            info.ForEach(p =>
            {
                Contract.Customer ticket = new contract.Customer
                {
                    CustomerName = p.CustomerName,
                    HeadImgUrl = p.HeadImgUrl,
                    PhoneNumber=p.CustomerPhone,
                    ReceivedStartTime = p.CreatedDate,
                    EventContent = p.EventLocation
                };

                list.Add(ticket);
            });

            return list;
        }

    }
}
