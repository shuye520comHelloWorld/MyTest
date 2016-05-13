using BeautyContestTW.Entity.Entities;
using System;
using ServiceStack.OrmLite;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace BeautyContestTW.WindowsService.Entity.Operations
{
    public class AppointmentOperation : BaseEntityOperation<Appointment>
    {
        public AppointmentOperation(string dbStr) : base(dbStr) { }

        public int UpdateOnlyContactIdByAppointmentKey(string appointmentKey, long resultContactId)
        {
            using (IDbConnection dbConn = dbFactory.OpenDbConnection())
            {
                
                return dbConn.UpdateNonDefaults(new Appointment { ContactId = resultContactId,UpdateDateTime=DateTime.Now },t => t.AppointmentKey == new Guid(appointmentKey));
               
            }
        }       
        public DataSet GetContactId(string connString, string spName, params SqlParameter[] parameters)
        {
            return RepositoryHelper.ExecuteDataset(connString, spName, parameters);
        }


    }
}
