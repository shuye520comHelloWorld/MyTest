using BeautyContestTW.Entity.Entities;
using BeautyContestTW.WindowsService.Entity.Entity;
using ServiceStack.OrmLite;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeautyContestTW.WindowsService.Entity.Operations
{
    public class MyConsultantsOperation : BaseEntityOperation<MyConsultants>
    {
       public MyConsultantsOperation(string dbStr) : base(dbStr) { }
       public async Task<List<MyConsultants>> GetDataByContactIds(List<long> ContactIds)
       {
           var result = new List<MyConsultants>();
           if (ContactIds != null && ContactIds.Count > 0)
           {
               using (IDbConnection dbConn = dbFactory.OpenDbConnection())
               {
                   SqlExpression<MyConsultants> ev = OrmLiteConfig.DialectProvider.SqlExpression<MyConsultants>();
                   ev.Where(t => Sql.In(t.ContactID, ContactIds));                   
                   var query =await dbConn.SelectAsync(ev);
                   if (query.Any())
                   {
                       result = query;
                   }

               }
           }
           return result;
       }
    }
}
