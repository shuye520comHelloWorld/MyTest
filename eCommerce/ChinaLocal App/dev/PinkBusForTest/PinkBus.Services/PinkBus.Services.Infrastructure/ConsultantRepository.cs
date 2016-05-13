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
using contract=PinkBus.Services.Contract;
using System.Net;
using ServiceStack;
namespace PinkBus.Services.Infrastructure
{
    public class ConsultantRepository :BaseRepository,IConsultantRepository
    {
        private const string SP_GetUnitBcList = "dbo.PB_GetUnitBCList";
        private ConsultantOperation consultantOperation;
        private EventOperation eventOperation;
        public ConsultantRepository()
        {
            consultantOperation = new ConsultantOperation(GlobalAppSettings.Community);
            eventOperation = new EventOperation(GlobalAppSettings.Community);
        }
       
        public GetProfileResponse GetProfile(GetProfile dto)
        { 
            EventBaseInfo baseInfo = base.GetEventBaseInfo(dto.EventKey);

            Consultant consultant = consultantOperation.GetSingleData(p => p.ContactId == dto._UserId && p.EventKey == dto.EventKey);
            if (consultant == null)
            {
                throw new NotFoundException(string.Format("consultant profile not found, event key {0}", dto.EventKey));
            }
            //TODO: countyList
            GetProfileResponse response = new GetProfileResponse
            {
                EventBaseInfo = baseInfo,
                ConsultantPhone = consultant.PhoneNumber,
                DirectSellerId = consultant.DirectSellerId,
                Province = consultant.Province,
                City = consultant.City,
                FirstName = consultant.FirstName,
                LastName = consultant.LastName,
                County =consultant.CountyName,
                CountyList = base.GetCountyList(consultant.City)
            };

            return response;
        }

        public UpdateProfileResponse UpdateProfile(UpdateProfile dto)
        {
            Consultant consultant = consultantOperation.GetSingleData(p => p.ContactId == dto._UserId && p.EventKey == dto.EventKey);
                      
            if (consultant == null)
            {
                 throw new NotFoundException(string.Format("consultant not found, event key {0}",dto.EventKey));
            }
            else
            {
                consultant.CountyName = dto.CountyName;
                consultant.OECountyId = dto.CountyCode.ToString();
                consultant.IsConfirmed = true;
                consultant.UpdatedDate = DateTime.Now;
                consultant.UpdatedBy = dto._UserId.ToString();
                consultantOperation.Update(consultant);
            }
            return new UpdateProfileResponse { Result = true };

        }

        public QueryUnitsBCResponse QueryUnitsBC(QueryUnitsBC dto)
        {
            var parameters = new SqlParameter[]
                        {                         
                            new SqlParameter("@ContactID",dto._UserId.ToString()),                         
                        };

            var result = RepositoryHelper.Query<PinkBus.Services.Infrastructure.Common.UnitBCInfo>(GlobalAppSettings.Community, SP_GetUnitBcList,
                parameters);

            List<contract.UnitBCInfo> unitBcInfoList = new List<contract.UnitBCInfo>();
            QueryUnitsBCResponse response = new QueryUnitsBCResponse();
            foreach (var item in result)
            {
                contract.UnitBCInfo unitBc = new contract.UnitBCInfo
                {
                    PhoneNumber = item.PhoneNumber,
                    ContactId = item.ContactId,
                    FirstName = item.FirstName,
                    LastName = item.LastName,
                    DirectSellerId = item.DirectSellerId                    
                };
                unitBcInfoList.Add(unitBc);
                response.UnitName = item.UnitName;
            }
            //excluding bc 
            response.UnitsBCInfo = unitBcInfoList.FindAll(p => p.ContactId != dto._UserId); 
            return response;
        }

    }
}
