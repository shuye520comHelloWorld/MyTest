using PinkBus.Services.Contract;
using PinkBus.Services.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using entity = PinkBus.Services.Entity;
using contract = PinkBus.Services.Contract;
namespace PinkBus.Services.Infrastructure.Mapper
{
    public static class CustomerMapper
    {
        public static contract.Customer CustomerToResponse(this Entity.Customer customer)
        {
            contract.Customer response = new contract.Customer();

            response = new contract.Customer
           {
               AgeRange = (AgeRange)customer.AgeRange,
               BeautyClass = customer.BeautyClass,
               Career = customer.Career.HasValue ? (Career)customer.Career : new Nullable<Career>(),
               CustomerName = customer.CustomerName,
               CustomerPhone = customer.CustomerPhone,
               CustomerType = (CustomerType)customer.CustomerType,
               InterestInCompany = customer.InterestInCompany,
               InterestingTopic = customer.InterestingTopic,
               UsedProduct = customer.UsedProduct.HasValue ? customer.UsedProduct : new Nullable<bool>(),
              
               UsedSet = customer.UsedSet,
               CustomerKey = customer.CustomerKey,
               IsImportMyCustomer = customer.IsImportMyCustomer,
               CreatedDate = customer.CreatedDate
           };


            return response;
        }
    }
}
