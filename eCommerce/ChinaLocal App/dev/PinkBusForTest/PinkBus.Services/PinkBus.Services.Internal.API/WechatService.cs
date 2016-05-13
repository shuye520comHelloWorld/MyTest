using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PinkBus.Services.Internal.Interface;
using PinkBus.Services.Internal.Contract;
namespace PinkBus.Services.Internal.API
{
    public class WechatService:  ServiceStack.Service
    {
        private IWechatServiceRepository wechatServiceRepository;

        public WechatService(IWechatServiceRepository wechatServiceRepository) 
        {
            this.wechatServiceRepository = wechatServiceRepository;
        }

        public QueryCustomerResponse Get(QueryCustomer dto)
        {
            return wechatServiceRepository.QueryCustomer(dto);
        }

        public QueryTicketResponse Get(QueryTicket dto)
        {
            return wechatServiceRepository.QueryTicket(dto);
        }

    }
}
