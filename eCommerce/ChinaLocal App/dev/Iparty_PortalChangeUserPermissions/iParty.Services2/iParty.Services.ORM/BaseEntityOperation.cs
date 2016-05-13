using ServiceStack.OrmLite;
using ServiceStack.OrmLite.SqlServer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace iParty.Services.ORM
{
    public class BaseEntityOperation<Entity> where Entity : new()
    {
        public OrmLiteConnectionFactory dbFactory { get; set; }

        public BaseEntityOperation(string ConnectString)
        {
            dbFactory = new OrmLiteConnectionFactory(ConnectString, SqlServerOrmLiteDialectProvider.Instance);
            OrmLiteConfig.DialectProvider.UseUnicode = true;
        }

        public virtual void Create(Entity t)
        {
            using (IDbConnection dbConn = dbFactory.OpenDbConnection())
            {
                dbConn.Insert(t);
            }
        }

        public virtual void Create(Entity t, IDbConnection db)
        {
            db.Insert(t);
        }

        public virtual void CreateByTransaction(Entity t, Action<IDbConnection, Entity> func)
        {
            using (IDbConnection dbConn = dbFactory.OpenDbConnection())
            using (IDbTransaction trans = dbConn.OpenTransaction())
            {
                dbConn.Insert(t);
                func(dbConn, t);
                trans.Commit();
            }
        }

        public virtual void CreateBatchData(IEnumerable<Entity> ts)
        {
            using (IDbConnection dbConn = dbFactory.OpenDbConnection())
            {
                dbConn.InsertAll(ts);
            }
        }

        public virtual void Update(Entity t)
        {
            using (IDbConnection dbConn = dbFactory.OpenDbConnection())
            {
                dbConn.Update(t);
            }
        }

        public virtual void UpdateBatch(IEnumerable<Entity> ts)
        {
            using (IDbConnection dbConn = dbFactory.OpenDbConnection())
            {
                dbConn.UpdateAll(ts);
            }
        }

        public virtual void UpdateBatch(IEnumerable<Entity> ts, IDbConnection db)
        {
            db.UpdateAll(ts);
        }

        public virtual void Update(Entity t, IDbConnection db)
        {
            db.Update(t);
        }

        public virtual void UpdateByTransaction(Entity t, Action<IDbConnection, Entity> func)
        {
            using (IDbConnection dbConn = dbFactory.OpenDbConnection())
            using (IDbTransaction trans = dbConn.OpenTransaction())
            {
                dbConn.Update(t);
                func(dbConn, t);
                trans.Commit();
            }
        }

        public virtual void DeleteById(object t)
        {
            using (IDbConnection dbConn = dbFactory.OpenDbConnection())
            {
                dbConn.DeleteById<Entity>(t);
            }
        }

        public virtual int DeleteByFilter(string where = null)
        {
            using (IDbConnection dbConn = dbFactory.OpenDbConnection())
            {
                return dbConn.Delete<Entity>(where);
            }
        }

        public virtual int DeleteByFunc(Expression<Func<Entity, bool>> Predicate)
        {
            using (IDbConnection dbConn = dbFactory.OpenDbConnection())
            {
                return dbConn.Delete<Entity>(Predicate);
            }
        }

        public virtual List<Entity> GetDataByFunc(Expression<Func<Entity, bool>> Predicate)
        {
            using (IDbConnection dbConn = dbFactory.OpenDbConnection())
            {
                return dbConn.Select(dbConn.From<Entity>().Where(Predicate));
            }
        }

        /// <summary>
        /// return current Entity list using procedure 
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public virtual List<Entity> ExecEntityProcedure(string sql, object value)
        {
            using (IDbConnection dbConn = dbFactory.OpenDbConnection())
            {
                return dbConn.SqlList<Entity>(sql, value);
            }
        }

        /// <summary>
        /// return int result.
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public virtual List<int> ExecintProcedure(string sql, object value)
        {
            using (IDbConnection dbConn = dbFactory.OpenDbConnection())
            {
                return dbConn.SqlList<int>(sql, value);
            }
        }


        public virtual void ExecProcedure(string sql, object value)
        {
            using (IDbConnection dbConn = dbFactory.OpenDbConnection())
            {
                dbConn.SqlProc(sql, value);
            }
        }

        public virtual void DeleteByIds(IEnumerable<object> ts)
        {
            using (IDbConnection dbConn = dbFactory.OpenDbConnection())
            {
                dbConn.DeleteByIds<Entity>(ts);
            }
        }

        /// <summary>
        /// Returns results from using a single name, value filter. E.g:('Age',11)
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public virtual Entity GetSingleData(string name, object value)
        {
            using (IDbConnection dbConn = dbFactory.OpenDbConnection())
            {
                return dbConn.SingleWhere<Entity>(name, value);
            }
        }

        public virtual Entity GetSingleDataByFunc(Expression<Func<Entity, bool>> predicate)
        {
            using (IDbConnection dbConn = dbFactory.OpenDbConnection())
            {
                return dbConn.Single<Entity>(predicate);
            }
        }

        /// <summary>
        /// Returns result from using key1=value1 'or' key2=value2
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public virtual List<Entity> GetDataByOrFileds(Dictionary<string, object> data)
        {
            if (data == null) return null;
            var result = new List<Entity>();
            foreach (KeyValuePair<string, object> kvp in data)
            {
                result.Add(GetSingleData(kvp.Key, kvp.Value));
            }
            return result;
        }

        public virtual List<Entity> GetAllData()
        {
            using (IDbConnection dbConn = dbFactory.OpenDbConnection())
            {
                return dbConn.Select<Entity>();
            }
        }

        public virtual List<Entity> GetDataByRawSql(string sql)
        {
            using (IDbConnection dbConn = dbFactory.OpenDbConnection())
            {
                return dbConn.Select<Entity>(sql);
            }
        }

        public virtual int ExecuteNoQuery(string sql)
        {
            using (IDbConnection dbConn = dbFactory.OpenDbConnection())
            {
                return dbConn.ExecuteSql(sql);
            }
        }

        public virtual long Count(Expression<Func<Entity, bool>> expression)
        {
            using (IDbConnection dbConn = dbFactory.OpenDbConnection())
            {
                return dbConn.Count(expression);
            }
        }

        public virtual List<NewEntity> SelectBySqlFmt<NewEntity>(string sql, params object[] sqlParams) where NewEntity : new()
        {
            using (IDbConnection dbConn = dbFactory.OpenDbConnection())
            {
                return dbConn.SelectFmt<NewEntity>(sql, sqlParams);
            }
        }

        public virtual List<NewEntity> Join<NewEntity, JoinWith>(
            Expression<Func<Entity, JoinWith, bool>> expression)
            where NewEntity : new()
            where JoinWith : new()
        {
            using (IDbConnection dbConn = dbFactory.OpenDbConnection())
            {
                return dbConn.Select<NewEntity>(dbConn.From<Entity>().Join<JoinWith>(expression));
            }
        }
        public virtual List<NewEntity> Join<NewEntity, JoinWith, WhereEntity>(
            Expression<Func<Entity, JoinWith, bool>> expression,
            Expression<Func<WhereEntity, bool>> predicate)
            where NewEntity : new()
            where JoinWith : new()
            where WhereEntity : new()
        {
            using (IDbConnection dbConn = dbFactory.OpenDbConnection())
            {
                return dbConn.Select<NewEntity>(dbConn.From<Entity>().Join<JoinWith>(expression).Where<WhereEntity>(predicate));
            }
        }

        public virtual List<NewEntity> Join<NewEntity, JoinWith, WhereSource, WhereTarget>(
            Expression<Func<Entity, JoinWith, bool>> expression,
            Expression<Func<WhereSource, WhereTarget, bool>> predicate)
            where NewEntity : new()
            where JoinWith : new()
            where WhereSource : new()
            where WhereTarget : new()
        {
            using (IDbConnection dbConn = dbFactory.OpenDbConnection())
            {
                return dbConn.Select<NewEntity>(dbConn.From<Entity>().Join<JoinWith>(expression).Where<WhereSource, WhereTarget>(predicate));
            }
        }

        public virtual List<NewEntity> LeftJoin<NewEntity, JoinWith>(
            Expression<Func<Entity, JoinWith, bool>> expression)
            where NewEntity : new()
            where JoinWith : new()
        {
            using (IDbConnection dbConn = dbFactory.OpenDbConnection())
            {
                return dbConn.Select<NewEntity>(dbConn.From<Entity>().LeftJoin<JoinWith>(expression));
            }
        }
        public virtual List<NewEntity> LeftJoin<NewEntity, JoinWith, WhereEntity>(
            Expression<Func<Entity, JoinWith, bool>> expression,
            Expression<Func<WhereEntity, bool>> predicate)
            where NewEntity : new()
            where JoinWith : new()
            where WhereEntity : new()
        {
            using (IDbConnection dbConn = dbFactory.OpenDbConnection())
            {
                return dbConn.Select<NewEntity>(dbConn.From<Entity>().LeftJoin<JoinWith>(expression).Where<WhereEntity>(predicate));
            }
        }

        public virtual List<NewEntity> LeftJoin<NewEntity, JoinWith, WhereSource, WhereTarget>(
            Expression<Func<Entity, JoinWith, bool>> expression,
            Expression<Func<WhereSource, WhereTarget, bool>> predicate)
            where NewEntity : new()
            where JoinWith : new()
            where WhereSource : new()
            where WhereTarget : new()
        {
            using (IDbConnection dbConn = dbFactory.OpenDbConnection())
            {
                return dbConn.Select<NewEntity>(dbConn.From<Entity>().LeftJoin<JoinWith>(expression).Where<WhereSource, WhereTarget>(predicate));
            }
        }

        public virtual List<NewEntity> RightJoin<NewEntity, JoinWith>(
            Expression<Func<Entity, JoinWith, bool>> expression)
            where NewEntity : new()
            where JoinWith : new()
        {
            using (IDbConnection dbConn = dbFactory.OpenDbConnection())
            {
                return dbConn.Select<NewEntity>(dbConn.From<Entity>().RightJoin<JoinWith>(expression));
            }
        }
        public virtual List<NewEntity> RightJoin<NewEntity, JoinWith, WhereEntity>(
            Expression<Func<Entity, JoinWith, bool>> expression,
            Expression<Func<WhereEntity, bool>> predicate)
            where NewEntity : new()
            where JoinWith : new()
            where WhereEntity : new()
        {
            using (IDbConnection dbConn = dbFactory.OpenDbConnection())
            {
                return dbConn.Select<NewEntity>(dbConn.From<Entity>().RightJoin<JoinWith>(expression).Where<WhereEntity>(predicate));
            }
        }

        public virtual List<NewEntity> RightJoin<NewEntity, JoinWith, WhereSource, WhereTarget>(
            Expression<Func<Entity, JoinWith, bool>> expression,
            Expression<Func<WhereSource, WhereTarget, bool>> predicate)
            where NewEntity : new()
            where JoinWith : new()
            where WhereSource : new()
            where WhereTarget : new()
        {
            using (IDbConnection dbConn = dbFactory.OpenDbConnection())
            {
                return dbConn.Select<NewEntity>(dbConn.From<Entity>().RightJoin<JoinWith>(expression).Where<WhereSource, WhereTarget>(predicate));
            }
        }
    }
}
