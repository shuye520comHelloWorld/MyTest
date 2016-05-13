using BeautyContestTW.Entity.Entities;
using ServiceStack.OrmLite;
using ServiceStack.OrmLite.SqlServer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BeautyContestTW.WindowsService.Entity.Operations
{
   public class AppointmentTrackerOperation:BaseEntityOperation<AppointmentTracker>
    {
       public AppointmentTrackerOperation(string dbStr) : base(dbStr) { }     
       public async Task<int> DeleteAsyncByFunc(Expression<Func<AppointmentTracker, bool>> Predicate)
       {
           using (IDbConnection dbConn = dbFactory.OpenDbConnection())
           {
               return await dbConn.DeleteAsync(Predicate);
           }
       }
    }
}
