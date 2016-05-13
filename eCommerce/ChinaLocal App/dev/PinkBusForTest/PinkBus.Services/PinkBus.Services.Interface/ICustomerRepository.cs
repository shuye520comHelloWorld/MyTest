using PinkBus.Services.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PinkBus.Services.Interface
{
    public interface ICustomerRepository
    {
        GetCustomerDetailResponse GetCustomerDetail(GetCustomerDetail dto);

        UpdateCustomerResponse UpdateCustomer(UpdateCustomer dto);

        QueryAssistantCustomerResponse QueryAssistantCustomers(QueryAssistantCustomers dto);
    }
}
