using iParty.Services.Entity;
using iParty.Services.Interface;
using iParty.Services.ORM.Operations;
using iParty.Services.ORM;
using QuartES.Services.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace iParty.Services.Infrastructure.Providers
{
    public class WorkshopsProvider : IWorkshopsProvider
    {
        private IContext _context;
        private const string GetWorkShopInfoByWorkShopId = "exec dbo.GetWorkShopInfoByWorkShopId @ShopId";
        private const string GetWorkShopInfoByContactId = "exec dbo.GetWorkShopInfoByContactId @ContactId";
        private const string GetWorkShopPartnersByWorkShopId = "exec GetWorkShopPartnersByWorkShopId @ShopId,@EventKey ";
        private WorkShopOperation _workShopOperation;
        private WorkShopOperation WorkShopOperation
        {
            get
            {
                if (_workShopOperation == null)
                {
                    _workShopOperation = new WorkShopOperation();
                }
                return _workShopOperation;
            }
        }
        private WorkShopUniteeOperation _workShopUniteeOperation;
        private WorkShopUniteeOperation WorkShopUniteeOperation
        {
            get
            {
                if (_workShopUniteeOperation == null)
                {
                    _workShopUniteeOperation = new WorkShopUniteeOperation();
                }
                return _workShopUniteeOperation;
            }
        }
        public WorkshopsProvider(IContext context)
        {
            _context = context;

        }

        private const string WORKSHOPKEY = "iParty_WorkshopKey_";



        private const int cachedMiniutes = 60;

        /// <summary>
        /// get the workshop info by ContactID
        /// </summary>
        /// <param name="contactId"></param>
        /// <returns></returns>
        public IEnumerable<Workshop> GetWorkshops(Guid eventKey)
        {
            var cacheKey = WORKSHOPKEY + _context.User.ContactID().ToString() + eventKey.ToString();
            var workShopList = new List<Workshop>();
            var workShopEntityList = CacheHelper.Get<List<WorkShopEntity>>(cacheKey);
            if (workShopEntityList == null || workShopEntityList.Count == 0)
            {
                workShopEntityList = WorkShopOperation.ExecEntityProcedure(GetWorkShopInfoByContactId, new { ContactID = _context.User.ContactID() });
                CacheHelper.Insert<List<WorkShopEntity>>(cacheKey, workShopEntityList, cachedMiniutes);

            }
            workShopList = new List<Workshop>();
            foreach (var item in workShopEntityList)
            {
                var workshop = BuildWorkShop(item, eventKey);
                workShopList.Add(workshop);
            }
            return workShopList;
        }


        /// <summary>
        /// get work shop info by WorkShop Id
        /// </summary>
        /// <param name="WorkshopId"></param>
        /// <returns></returns>
        public Workshop GetWorkshopById(int WorkshopId, Guid eventKey)
        {
            var cacheKey = WORKSHOPKEY + WorkshopId.ToString() + eventKey.ToString();
            var workShops = CacheHelper.Get<List<WorkShopEntity>>(cacheKey);
            if (workShops == null || workShops.Count == 0)
            {
                workShops = WorkShopOperation.ExecEntityProcedure(GetWorkShopInfoByWorkShopId, new { ShopId = WorkshopId });
                CacheHelper.Insert<List<WorkShopEntity>>(cacheKey, workShops, cachedMiniutes);
            }

            var item = workShops.FirstOrDefault();
            if (item == null)
            {
                return null;
            }
            var workshop = BuildWorkShop(item, eventKey);
            return workshop;
            //   throw new NotImplementedException();
        }

        private Workshop BuildWorkShop(WorkShopEntity item, Guid eventKey)
        {
            var workshop = new Workshop();
            workshop.WorkshopId = item.Recordid;
            if (item.TypeValue == 0)
            {
                workshop.Type = WorkshopType.Normal;
            }
            else
            {
                workshop.Type = WorkshopType.Award;
            }
            workshop.Name = item.ShopLicenseName;
            workshop.Location = item.ShopAddress;

            var coHostConsulants = new List<CoHostConsultant>();
            var unitees = WorkShopUniteeOperation.ExecEntityProcedure(GetWorkShopPartnersByWorkShopId, new { ShopId = workshop.WorkshopId, EventKey = eventKey });

            foreach (var unitee in unitees)
            {
                var coHostConsultant = new CoHostConsultant();
                coHostConsultant.ContactId = unitee.ContactId;
                coHostConsultant.LastName = unitee.LastName;
                coHostConsultant.FirstName = unitee.FirstName;
                coHostConsultant.Level = int.Parse(unitee.LevelCode);
                coHostConsultant.StartDate = unitee.StartDate;
                coHostConsultant.IsOwner = unitee.IsHost;
                coHostConsultant.IsApplied = unitee.IsApplied;
                coHostConsultant.UnitId = unitee.UnitId;
                coHostConsulants.Add(coHostConsultant);
            }
            workshop.Consultants = coHostConsulants;
            return workshop;
        }

        private const string GETUNSABLENESSCONSULTANTS = "exec ipartySP_GetUsablenessConsultants @WorkShopId,@EventKey";
        public List<long> GetUsablenessConsultants(int workShopId, Guid eventKey)
        {
            var operation = new BaseEntityOperation<long>(ConfigurationManager.AppSettings["Community"]);

            var result = operation.ExecEntityProcedure(GETUNSABLENESSCONSULTANTS, new { WorkShopId = workShopId, EventKey = eventKey });
            return result;
        }
    }
}
