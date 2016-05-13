using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using Newtonsoft.Json;
using NLog;

namespace PinkBus.ApplyBCTicket.WindowsService
{
    public class DAO
    {
        readonly static Logger _logger = LogManager.GetCurrentClassLogger();

        public static List<PB_ApplyTicketTracker> queryTicketTrackers()
        {
            string sql = "dbo.PB_QueryPendingTicketTracker";
            DataSet ds = SqlHelper.ExecuteDataset(AppSetting.Community, sql, AppSetting.PendingSize);
            List<PB_ApplyTicketTracker> res = JsonConvert.DeserializeObject<List<PB_ApplyTicketTracker>>(JsonConvert.SerializeObject(ds.Tables[0]));

            return res;
        }

        public static void ApplyTickets(PB_ApplyTicketTracker T)
        {
            try
            {
                string sql = "dbo.[PB_SaveApplyTicket]";
                SqlParameter[] Params = new SqlParameter[]
            {
                new SqlParameter("@TrackerKey",T.TrackerKey),
                new SqlParameter("@EventKey",T.EventKey),
                new SqlParameter("@SessionKey",T.SessionKey),
                new SqlParameter("@MappingKey",T.MappingKey),
                new SqlParameter("@CreatedDate",DateTime.Now),
                new SqlParameter("@Result",SqlDbType.Bit),
                 new SqlParameter("@Descript",SqlDbType.VarChar,1000)
            };

                Params[5].Direction = ParameterDirection.Output;
                Params[6].Direction = ParameterDirection.Output;
                DataSet ds = SqlHelper.ExecuteDataset(AppSetting.Community, CommandType.StoredProcedure, sql, Params);

                bool result = bool.Parse(Params[5].Value.ToString());
                string descript = Params[6].Value.ToString();

                if (!result)
                {
                    _logger.Error(descript);
                }

            }
            catch (Exception ex)
            {
                _logger.Debug(ex.ToString());
                _logger.Error(ex.ToString());
            }
        }
    }
}
