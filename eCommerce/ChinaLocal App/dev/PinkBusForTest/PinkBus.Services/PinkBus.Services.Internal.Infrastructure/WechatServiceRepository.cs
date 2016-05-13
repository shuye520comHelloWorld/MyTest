using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PinkBus.Services.Internal.Interface;

using PinkBus.Services.Entity;
using PinkBus.Services.Common;
using PinkBus.Services.Entity.Operation;
using PinkBus.Services.Internal.Contract;
using System.Data.SqlClient;
using contract=PinkBus.Services.Internal.Contract;
using PinkBus.Services.Internal.Infrastructure.Common;
namespace PinkBus.Services.Internal.Infrastructure
{
    public class WechatServiceRepository : IWechatServiceRepository
    {
        private CustomerOperation customerOperation;
        private TicketOperation ticketOperation;
        private const string SP_PB_GetCustomerList = "PB_Customer_GetList";
        private const string SP_PB_GetTicketList = "PB_Ticket_GetListByUnionID";

        public WechatServiceRepository()
        {
            customerOperation = new CustomerOperation(GlobalAppSettings.Community);
            ticketOperation = new TicketOperation(GlobalAppSettings.Community);
        }

        public QueryCustomerResponse QueryCustomer(QueryCustomer dto)
        { 
            var parameters = new SqlParameter[]
                        {                         
                            new SqlParameter("@ContactId",dto.ContactId)                               
                        };

            var customerInfo = RepositoryHelper.Query<CustomerInfo>(GlobalAppSettings.Community, SP_PB_GetCustomerList,
              parameters).ToList();         
            
         
            return new QueryCustomerResponse
            {
                Customers=customerInfo.CustomerInfoToResponse()
            };
        }

        public QueryTicketResponse QueryTicket(QueryTicket dto)
        {
            var parameters = new SqlParameter[]
                        {                         
                            new SqlParameter("@UnionId",dto.UnionID)                               
                        };

            var ticketInfo = RepositoryHelper.Query<TicketInfo>(GlobalAppSettings.Community, SP_PB_GetTicketList,
              parameters).ToList();            
          
            return new QueryTicketResponse
            {
                Tickets = ticketInfo.TicketInfoToResponse()
            };
        }

    }
    
}
