using PinkBus.CheckInClient.DAL;
using PinkBus.CheckInClient.Entitys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PinkBus.CheckInClient.Helper
{
    public class DataSourceHelper
    {
        public static List<CustomerTicket> DataGridViewDataSource(Guid eventKey)
        {
            List<CustomerTicket> customers = EventDAL.QueryCustomers(eventKey.ToString());
            List<ConsultantTicket> volunteers = EventDAL.QueryVolunteers(eventKey.ToString());

            customers = customers.OrderBy(i => i.TicketType).ToList();

            volunteers.ForEach(e =>
            {
                CustomerTicket cTicket = new CustomerTicket();
                cTicket.TicketKey = e.DirectSellerId.ToString();
                cTicket.SMSToken = e.ContactId.ToString();
                cTicket.CustomerKey = e.MappingKey;
                cTicket.TicketType = TicketType.Volunteer;
                cTicket.CustomerName = e.LastName + e.FirstName;
                cTicket.CustomerPhone = e.PhoneNumber;
                cTicket.CheckinCount = e.Status == (int)ConsultantStatus .CheckedIn&& (e.CheckinCount == 0 || e.CheckinCount == null) ? 0 : e.CheckinCount;
                cTicket.TicketStatus = e.Status == (int)ConsultantStatus.Canceled ? TicketStatus.Canceled : TicketStatus.Created;
                customers.Add(cTicket);

            });

            return customers;
        }
    }
}
