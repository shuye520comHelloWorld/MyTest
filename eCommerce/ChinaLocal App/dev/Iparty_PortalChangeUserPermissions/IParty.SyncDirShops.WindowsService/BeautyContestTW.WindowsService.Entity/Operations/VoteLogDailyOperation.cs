using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using ServiceStack.OrmLite;
using ServiceStack.OrmLite.SqlServer;
using BeautyContestTW.Entity.Entities;

namespace BeautyContestTW.WindowsService.Entity.Operations
{
    public class VoteLogDailyOperation : BaseEntityOperation<VoteLogDaily>
    {
        public VoteLogDailyOperation(string str) : base(str) { }

        //public async  Task<List<VoteLogDaily>> GetDataTopNumAsync(int topCount,DateTime timeNow,Guid PieceKey)
        //{
        //    using (IDbConnection dbConn = dbFactory.OpenDbConnection())
        //    {
        //        return await dbConn.SelectAsync<VoteLogDaily>(dbConn.From<VoteLogDaily>().Take(topCount).Where(x => x.CreateDateTime < timeNow.Date && x.PieceKey == PieceKey));
        //    }
        //}

        public async Task<List<VoteLogDaily>> getPieceKeyGroupBy()
        {
            try
            {
                using (IDbConnection dbConn = dbFactory.OpenDbConnection())
                {
                    using (IDbTransaction idbt = dbConn.OpenTransaction(IsolationLevel.ReadCommitted))
                    {
                        
                        return await dbConn.SelectAsync<VoteLogDaily>(dbConn.From<VoteLogDaily>().GroupBy(x => x.PieceKey).Select(e => e.PieceKey));
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return null;
            }
        }
        public async Task<List<VoteLogDaily>> getUserGroupBy()
        {
            try
            {
                using (IDbConnection dbConn = dbFactory.OpenDbConnection())
                {
                    using (IDbTransaction idbt = dbConn.OpenTransaction(IsolationLevel.ReadCommitted))
                    {

                        return await dbConn.SelectAsync<VoteLogDaily>(dbConn.From<VoteLogDaily>().GroupBy(x => x.UserKey).Select(e => e.UserKey));
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return null;
            }
        }
        
    }
}
