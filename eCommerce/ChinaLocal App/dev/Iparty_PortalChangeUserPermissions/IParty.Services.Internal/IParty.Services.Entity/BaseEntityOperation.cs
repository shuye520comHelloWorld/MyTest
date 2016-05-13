using IParty.Services.Common;
using ServiceStack.OrmLite;
using ServiceStack.OrmLite.SqlServer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace IParty.Services.Entity
{
    public class BaseEntityOperation<Entity> where Entity : new()
    {
        public OrmLiteConnectionFactory dbFactory { get; set; }

        private int pageSize;
        public long TotalCount { get; private set; }
        public int PageCount
        {
            get
            {
                return pageSize == 0 ? 0 : (int)Math.Ceiling((double)TotalCount / pageSize);
            }
        }

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

        public virtual long CreateWithReturn(Entity t)
        {
            using (IDbConnection dbConn = dbFactory.OpenDbConnection())
            {
                return dbConn.Insert(t, selectIdentity: true);
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
                return dbConn.Select<Entity>(Predicate);
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

        /// <summary>
        /// Return single entity data by expression
        /// </summary>
        /// <param name="Predicate"></param>
        /// <returns></returns>
        public virtual Entity GetSingleData(Expression<Func<Entity, bool>> Predicate)
        {
            using (IDbConnection dbConn = dbFactory.OpenDbConnection())
            {
                return dbConn.Select<Entity>(Predicate).FirstOrDefault();
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

        public virtual long GetRelationCount(Expression<Func<Entity, bool>> Predicate)
        {
            using (IDbConnection dbConn = dbFactory.OpenDbConnection())
            {
                return dbConn.Count<Entity>(Predicate);
            }
        }

        /// <summary>
        /// get data by paging with where condition
        /// </summary>
        /// <param name="orderbyPredicate">order by field</param>
        /// <param name="pageSize">page size</param>
        /// <param name="pageNumber">page number, start from 1.</param>
        /// <param name="isDesc">desc/asc</param>
        /// <returns></returns>
        public virtual List<Entity> GetPagedDataByFunc(Expression<Func<Entity, bool>> wherePredicate, Expression<Func<Entity, object>> orderbyPredicate, int pageSize, int pageNumber, bool? isDesc = null)
        {
            using (IDbConnection dbConn = dbFactory.OpenDbConnection())
            {

                //var orderByStr = string.Format("{0} {1}", orderByField, (isDesc.HasValue && isDesc.Value == false) ? "desc" : "asc");
                var dataList = new List<Entity>();

                if (isDesc != null && isDesc.Value == true)
                    dataList = dbConn.Select<Entity>(p => p.Where(wherePredicate).OrderByDescending(orderbyPredicate).Limit(skip: (pageNumber - 1) * pageSize, rows: pageSize));
                else
                    dataList = dbConn.Select<Entity>(p => p.Where(wherePredicate).OrderBy(orderbyPredicate).Limit(skip: (pageNumber - 1) * pageSize, rows: pageSize));

                this.TotalCount = dbConn.Count<Entity>(p => p.Where(wherePredicate));
                this.pageSize = pageSize;

                return dataList;
            }
        }


        /// <summary>
        /// get data by paging without where condition
        /// </summary>
        /// <param name="orderbyPredicate">order by field</param>
        /// <param name="pageSize">page size</param>
        /// <param name="pageNumber">page number, start from 1.</param>
        /// <param name="isDesc">desc/asc</param>
        /// <returns></returns>
        public virtual List<Entity> GetPagedDataByFunc(Expression<Func<Entity, object>> orderbyPredicate, int pageSize, int pageNumber, bool? isDesc = null)
        {
            using (IDbConnection dbConn = dbFactory.OpenDbConnection())
            {

                // var orderByStr = string.Format("{0} {1}", orderByField, (isDesc.HasValue && isDesc.Value == true) ? "desc" : "asc");
                var dataList = new List<Entity>();

                if (isDesc != null && isDesc.Value == true)
                    dataList = dbConn.Select<Entity>(p => p.OrderByDescending(orderbyPredicate).Limit(skip: (pageNumber - 1) * pageSize, rows: pageSize));
                else
                    dataList = dbConn.Select<Entity>(p => p.OrderBy(orderbyPredicate).Limit(skip: (pageNumber - 1) * pageSize, rows: pageSize));

                this.TotalCount = dbConn.Count<Entity>();
                this.pageSize = pageSize;

                return dataList;
            }
        }

        public virtual void SaveWithTransaction(Action<TransactionSave<Entity>> doSave, Entity mainData)
        {
            using (IDbConnection db = dbFactory.OpenDbConnection())
            {
                using (IDbTransaction trans = db.OpenTransaction(IsolationLevel.ReadCommitted))
                {
                    if (doSave != null)
                    {
                        doSave(new TransactionSave<Entity> { MainData = mainData, Db = db });
                        trans.Commit();
                    }
                }
            }
        }

        public virtual void SaveWithTransaction(Action<TransactionSave<Entity>> doSave, Entity mainData, BaseRequestDto dto)
        {
            using (IDbConnection db = dbFactory.OpenDbConnection())
            {
                using (IDbTransaction trans = db.OpenTransaction(IsolationLevel.ReadCommitted))
                {
                    if (doSave != null)
                    {
                        doSave(new TransactionSave<Entity> { MainData = mainData, Db = db, Dto = dto });
                        trans.Commit();
                    }
                }
            }
        }

        public virtual void SaveWithTransaction(Action<TransactionSave<Entity>> doSave, BaseRequestDto dto)
        {
            using (IDbConnection db = dbFactory.OpenDbConnection())
            {
                using (IDbTransaction trans = db.OpenTransaction(IsolationLevel.ReadCommitted))
                {
                    if (doSave != null)
                    {
                        doSave(new TransactionSave<Entity> { Db = db, Dto = dto });
                        trans.Commit();
                    }
                }
            }
        }

        public virtual void SaveWithTransaction(Action<TransactionSave<Entity>> doSave)
        {
            using (IDbConnection db = dbFactory.OpenDbConnection())
            {
                using (IDbTransaction trans = db.OpenTransaction(IsolationLevel.ReadCommitted))
                {
                    if (doSave != null)
                    {
                        doSave(new TransactionSave<Entity> { Db = db });
                        trans.Commit();
                    }
                }
            }
        }
    }
}
