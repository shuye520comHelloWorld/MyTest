using Newtonsoft.Json;
using PinkBus.ClientServices.Contract;
using PinkBus.ClientServices.Interface;
using PinkBus.Services.Common;
using PinkBus.Services.Entity;
using PinkBus.Services.Entity.Operation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PinkBus.ClientServices.Infrastructure
{
    public class UpLoadOfflineCustomerRepository : IUpLoadOfflineCustomerRepository
    {
        private OfflineCustomerOperation offlineCustomerOperation;
        private EventOperation eventOperation;
        public UpLoadOfflineCustomerRepository()
        {
            offlineCustomerOperation = new OfflineCustomerOperation(GlobalAppSettings.Community);
            eventOperation = new EventOperation(GlobalAppSettings.Community);
        }

        public UploadOfflineCustomerResponse UploadOfflineCustomer(UploadOfflineCustomer dto)
        {
            UploadOfflineCustomerResponse response = new UploadOfflineCustomerResponse();
            //var checkResult = eventOperation.GetDataByFunc(p => p.UploadToken == dto.UploadPassCode && p.EventKey == dto.EventKey).FirstOrDefault();
            //if (checkResult == null)
            //{
            //    response.Result = false;
            //    response.Message = "上传活动码有误";
            //    return response;
            //}
            try
            {
                OfflineCustomer customer = JsonConvert.DeserializeObject<OfflineCustomer>(dto.JsonData);
                customer.IsImport = false;
                customer.CreatedDate = DateTime.Now;
                customer.CreatedBy = "Upload";
                offlineCustomerOperation.Create(customer);
                response.Result = true;
            }
            catch (Exception ex)
            {
                response.Result = false;
                response.Message = ex.ToString().Replace("'", "‘");
            }
            return response;
        }
    }
}
