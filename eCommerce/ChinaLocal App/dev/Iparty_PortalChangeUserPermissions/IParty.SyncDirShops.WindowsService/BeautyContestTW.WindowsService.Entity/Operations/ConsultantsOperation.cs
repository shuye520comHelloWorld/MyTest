using BeautyContestTW.Entity.Entities;
using ServiceStack.OrmLite;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeautyContestTW.WindowsService.Entity.Operations
{
    public class ConsultantsOperation : BaseEntityOperation<Consultants>
    {
        public ConsultantsOperation(string dbStr) : base(dbStr) { }

        public List<Consultants> GetDataByContactIds(List<long> ContactIds,string status)
        {
            var result = new List<Consultants>();
            if (ContactIds != null && ContactIds.Count > 0)
            {
                using (IDbConnection dbConn = dbFactory.OpenDbConnection())
                {
                    SqlExpression<Consultants> ev = OrmLiteConfig.DialectProvider.SqlExpression<Consultants>();
                    ev.Where(t => Sql.In(t.ContactID, ContactIds) && t.ConsultantStatus.Contains(status));
                    var query =dbConn.Select(ev);
                    if (query.Any())
                    {
                        result = query.ToList();
                    }
                }
            }
            return result;
        }
        public DataSet GetConsultantBysp(string connString, string spName, params SqlParameter[] parameters)
        {
            return RepositoryHelper.ExecuteDataset(connString, spName, parameters);
        }

       
    }
}
