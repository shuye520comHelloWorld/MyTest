using PinkBus.Services.Common;
using PinkBus.Services.Entity.Operation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PinkBus.Services.Contract;
using PinkBus.Services.Interface;
using PinkBus.Services.Entity;
using PinkBus.Services.Infrastructure.Mapper;
using System.Data.SqlClient;
using PinkBus.Services.Infrastructure.Common;
using contract = PinkBus.Services.Contract;

namespace PinkBus.Services.Infrastructure
{
    public class CustomerRepository : BaseRepository, ICustomerRepository
    {
        private CustomerOperation customerOperation;
        private OfflineCustomerOperation offlineCustomerOperation;
        public CustomerRepository()
        {
            customerOperation = new CustomerOperation(GlobalAppSettings.Community);
            offlineCustomerOperation = new OfflineCustomerOperation(GlobalAppSettings.Community);
        }

        public GetCustomerDetailResponse GetCustomerDetail(GetCustomerDetail dto)
        {
            var customer = customerOperation.GetSingleData(p => p.CustomerKey == dto.CustomerKey).CustomerToResponse();

            if (customer == null)
                throw new NotFoundException(string.Format("cusomter not found, customer key {0}", dto.CustomerKey));

            return new GetCustomerDetailResponse
            {
                Customer = customer
            };
        }

        public UpdateCustomerResponse UpdateCustomer(UpdateCustomer dto)
        {
            var customer = customerOperation.GetSingleData(p => p.CustomerKey == dto.CustomerKey);//.CustomerToResponse();
            if (customer == null)
                throw new NotFoundException(string.Format("cusomter not found, customer key {0}", dto.CustomerKey));

            customer.IsImportMyCustomer = dto.IsImportMyCustomer;
            customer.UpdatedDate = DateTime.Now;
            customerOperation.Update(customer);

            return new UpdateCustomerResponse { Result = true };
        }

        public QueryAssistantCustomerResponse QueryAssistantCustomers(QueryAssistantCustomers dto)
        {
            List<OfflineCustomer> customers = new List<OfflineCustomer>();
            if (dto.TimeStamp != null && dto.TimeStamp.HasValue)
                customers = offlineCustomerOperation.GetDataByFunc(p => p.CreatedDate >= dto.TimeStamp.Value
                    && p.DirectSellerId == dto.DirectSellerId && p.IsImport==false).ToList();
            else
                customers = offlineCustomerOperation.GetDataByFunc(p => p.DirectSellerId == dto.DirectSellerId 
                    && p.IsImport==false).ToList();

            
            List<AssistantCustomer> assistantCustomer = new List<AssistantCustomer>();
            if (customers.Count > 0)
            {
                customers.ForEach((customer) =>
                {
                    AssistantCustomer assistant = new AssistantCustomer
                    {
                        CustomerName = customer.CustomerName,
                        ContactInfo = customer.ContactInfo,
                        ContactType = customer.ContactType,
                        Age = customer.AgeRange,
                        Career = customer.Career,
                        CustomerKey = customer.CustomerKey
                    };
                    assistantCustomer.Add(assistant);
                    customer.IsImport = true;
                });

            }
            offlineCustomerOperation.UpdateBatch(customers);
            return new QueryAssistantCustomerResponse { Customers = assistantCustomer };
        }
        //public Consultant GetConsultantInfo(Guid eventKey)
        //{
        //    string eventBaseInfoCacheKey = string.Format("event_key{0}", eventKey);

        //    EventBaseInfo baseInfo = CacheHelper.Get<EventBaseInfo>(eventBaseInfoCacheKey);
        //    if (baseInfo == null)
        //    {
        //        baseInfo = eventOperation.GetSingleData(p => p.EventKey == eventKey).ToEventBaseInfo();
        //        CacheHelper.Insert<EventBaseInfo>(eventBaseInfoCacheKey, baseInfo, cachedMiniutes);
        //    }
        //    return baseInfo;
        //}


    }
}
