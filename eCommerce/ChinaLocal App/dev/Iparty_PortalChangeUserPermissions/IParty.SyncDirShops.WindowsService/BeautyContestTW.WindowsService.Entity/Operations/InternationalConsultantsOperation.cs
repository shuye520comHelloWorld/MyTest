using BeautyContestTW.Entity.Entities;
using System;
using ServiceStack.OrmLite;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using BeautyContestTW.WindowsService.Entity.Entity;

namespace BeautyContestTW.WindowsService.Entity.Operations
{
    public class InternationalConsultantsOperation : BaseEntityOperation<InternationalConsultants>
    {
        public InternationalConsultantsOperation(string dbStr)
            : base(dbStr) { }
        public async Task<List<long>> GetContactId(int RegistrationProvince, int RegistrationCity, int RegistrationCounty)
        {
            using (IDbConnection dbConn = dbFactory.OpenDbConnection())
            {
                string sql=string.Format("select ContactId from dbo.InternationalConsultants where RegistrationProvince={0} and RegistrationCity={1} and RegistrationCounty={2}"
                    ,RegistrationProvince,RegistrationCity,RegistrationCounty);
                return await dbConn.SelectAsync<long>(sql);
            }
        }
        public async Task<List<long>> GetContactId(int RegistrationProvince, int RegistrationCity)
        {
            using (IDbConnection dbConn = dbFactory.OpenDbConnection())
            {
                string sql = string.Format("select ContactId from dbo.InternationalConsultants where RegistrationProvince={0} and RegistrationCity={1}"
                    , RegistrationProvince, RegistrationCity);
                return await dbConn.SelectAsync<long>(sql);
            }
        }
      
    }
}
