using PinkBus.Services.Contract;
using PinkBus.Services.Interface;
using PinkBus.Services.OAuth.Attribute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PinkBus.Services.API
{
    [OAuthRequest(ProjectType.Intouch)]
    public class CustomerService : ServiceStack.Service
    {
        private ICustomerRepository customerRepository;

        public CustomerService(ICustomerRepository customerRepository)
        {
            this.customerRepository = customerRepository;
        }

        public GetCustomerDetailResponse Get(GetCustomerDetail dto)
        {
            return customerRepository.GetCustomerDetail(dto);
        }

        public UpdateCustomerResponse Put(UpdateCustomer dto)
        {
            return customerRepository.UpdateCustomer(dto);
        }

        public UpdateCustomerResponse Post(UpdateCustomer dto)
        {
            return customerRepository.UpdateCustomer(dto);
        }

        public QueryAssistantCustomerResponse Get(QueryAssistantCustomers dto)
        {
            return customerRepository.QueryAssistantCustomers(dto);
        }

    }
}
