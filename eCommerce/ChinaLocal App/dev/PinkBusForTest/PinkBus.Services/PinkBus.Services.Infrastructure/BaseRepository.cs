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
using System.Data;
namespace PinkBus.Services.Infrastructure
{
    public class BaseRepository : IBaseRepository
    {
        private const string SP_GetCountyList = "PB_GetCountyListByCityName";
        private EventOperation eventOperation;
        private ConsultantOperation consultantOperation;
        private EventTicketSettingOperation eventTicketSettingOperation;
        public BaseRepository()
        {
            eventOperation = new EventOperation(GlobalAppSettings.Community);
            consultantOperation = new ConsultantOperation(GlobalAppSettings.Community);
            eventTicketSettingOperation = new EventTicketSettingOperation(GlobalAppSettings.Community);
        }
        private static int cachedMiniutes = 30;

        public EventBaseInfo GetEventBaseInfo(Guid eventKey)
        {
            return GetEventEntity(eventKey).ToEventBaseInfo();
        }

        public Event GetEventEntity(Guid eventKey)
        {
            string eventBaseInfoCacheKey = string.Format("eventBaseInfo_key{0}", eventKey);
            //EventBaseInfo baseInfo = CacheHelper.Get<EventBaseInfo>(eventBaseInfoCacheKey);
            Event baseInfo = CacheHelper.Get<Event>(eventBaseInfoCacheKey);
            if (baseInfo == null)
            {
                baseInfo = eventOperation.GetSingleData(p => p.EventKey == eventKey);
                if(baseInfo== null)
                    throw new NotFoundException(string.Format("event not found, event key {0}", eventKey));
                CacheHelper.Insert<Event>(eventBaseInfoCacheKey, baseInfo, cachedMiniutes);
            }
            return baseInfo;
        }

        public EventTicketSetting GetEventTicketSetting(Guid eventKey)
        {
            string eventTicketSettingCacheKey = string.Format("eventTicketSetting_{0}", eventKey);
            EventTicketSetting eventSettingInfo = CacheHelper.Get<EventTicketSetting>(eventTicketSettingCacheKey);
            if (eventSettingInfo == null)
            {
                eventSettingInfo = eventTicketSettingOperation.GetSingleData(p => p.EventKey == eventKey);
                
                CacheHelper.Insert<EventTicketSetting>(eventTicketSettingCacheKey, eventSettingInfo, cachedMiniutes);
            }
            return eventSettingInfo;
        }

        public List<County> GetCountyList(string cityName)
        {
            string countyListCacheKey = string.Format("Consultant_City_County{0}", cityName);

            List<County> countyList = CacheHelper.Get<List<County>>(countyListCacheKey);
            if (countyList == null)
            {
                countyList = GetCountyListFromDataBase(cityName);
                CacheHelper.Insert<List<County>>(countyListCacheKey, countyList, cachedMiniutes);
            }
            return countyList;
        }

        private List<County> GetCountyListFromDataBase(string cityName)
        {           
            List<County> county = new List<County>();
            var ds = SqlHelper.ExecuteDataset(GlobalAppSettings.Contacts, SP_GetCountyList, cityName);
            if (ds.Tables.Count > 0)
            {
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    county.Add(new County
                    {
                        CountyName = row["Name"].ToString(),
                        CountyCode = int.Parse(row["CountyID"].ToString())
                    });
                }
            }
            return county;
        }

        //public Event GetEventEntity(Guid eventKey)
        //{
        //    string eventBaseInfoCacheKey = string.Format("event_key{0}", eventKey);

        //    Event eventEntity = CacheHelper.Get<Event>(eventBaseInfoCacheKey);
        //    if (eventEntity == null)
        //    {
        //        eventEntity = eventOperation.GetSingleData(p => p.EventKey == eventKey);
        //        CacheHelper.Insert<Event>(eventBaseInfoCacheKey, eventEntity, cachedMiniutes);
        //    }
        //    return eventEntity;
        //}

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
