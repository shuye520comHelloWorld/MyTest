using ServiceStack.OrmLite;
using ServiceStack.OrmLite.SqlServer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using ServiceStack;
using ServiceStack.Text;
using System.Threading;

namespace BeautyContestTW.WindowsService.Entity
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

        //public virtual async Task<bool> SaveWithReturnAsync(Entity t)
        //{
        //    using (IDbConnection dbConn = dbFactory.OpenDbConnection())
        //    {
        //        return await dbConn.SaveAsync(t);
        //    }
        //}

        public virtual bool SaveWithReturn(Entity t)
        {
            using (IDbConnection dbConn = dbFactory.OpenDbConnection())
            {
                return dbConn.Save(t);
            }
        }

        //public virtual async Task UpdateOrInsertAsync(Entity t, Expression<Func<Entity, bool>> Predicate)
        //{
        //    using (IDbConnection dbConn = dbFactory.OpenDbConnection())
        //    {
        //        var _T = await dbConn.SingleAsync<Entity>(Predicate);
        //        if (_T==null)
        //             await dbConn.InsertAsync(t);
        //        else
        //             await dbConn.UpdateAsync(t);

        //    }
        //}

        public virtual void UpdateOrInsert(Entity t, Expression<Func<Entity, bool>> Predicate)
        {
            using (IDbConnection dbConn = dbFactory.OpenDbConnection())
            {
                var _T =  dbConn.Single<Entity>(Predicate);
                if (_T == null)
                     dbConn.Insert(t);
                else
                     dbConn.Update(t);

            }
        }

        public virtual void Create(Entity t)
        {
            using (IDbConnection dbConn = dbFactory.OpenDbConnection())
            {
                dbConn.Insert(t);
            }
        }


        //public virtual async Task CreateAsync(Entity t)
        //{
        //    using (IDbConnection dbConn = dbFactory.OpenDbConnection())
        //    {
        //        await dbConn.InsertAsync(t);
        //    }
        //}

        //public virtual async Task<long> CreateWithReturnAsync(Entity t)
        //{
        //    using (IDbConnection dbConn = dbFactory.OpenDbConnection())
        //    {
        //       return await dbConn.InsertAsync(t);
        //    }
        //}

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

        public virtual int UpdateBack(Entity t)
        {
            using (IDbConnection dbConn = dbFactory.OpenDbConnection())
            {
                return  dbConn.Update(t);
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

        //public virtual async Task  DeleteByIdAsync(object t)
        //{
        //    using (IDbConnection dbConn = dbFactory.OpenDbConnection())
        //    {
        //       await dbConn.DeleteByIdAsync<Entity>(t);
        //    }
        //}

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

        //public virtual async Task<int> DeleteByFuncAsync(Expression<Func<Entity, bool>> Predicate)
        //{
        //    using (IDbConnection dbConn = dbFactory.OpenDbConnection())
        //    {
        //        return await dbConn.DeleteAsync<Entity>(Predicate);
        //    }
        //}
       

        public virtual List<Entity> GetDataByFunc(Expression<Func<Entity, bool>> Predicate)
        {
            using (IDbConnection dbConn = dbFactory.OpenDbConnection())
            {
                return dbConn.Select<Entity>(Predicate);
            }
        }

        //public virtual async Task<List<Entity>> GetDataByFuncAsync(Expression<Func<Entity, bool>> Predicate)
        //{
        //    using (IDbConnection dbConn = dbFactory.OpenDbConnection())
        //    {
        //        return await dbConn.SelectAsync<Entity>(Predicate);
        //    }
        //}

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
        /// return current Entity list using procedure 
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public virtual List<Entity> ExecEntityProcedure(string sql)
        {
            using (IDbConnection dbConn = dbFactory.OpenDbConnection())
            {
                return dbConn.SqlList<Entity>(sql);
            }
        }

        /// <summary>
        /// return current Entity list using procedure 
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public virtual List<Entity> ExecEntityProcedure<Entity>(string sql, object value)
        {
            using (IDbConnection dbConn = dbFactory.OpenDbConnection())
            {
                return dbConn.SqlList<Entity>(sql, value);
            }
        }

        ///// <summary>
        ///// return current Entity list using procedure 
        ///// </summary>
        ///// <param name="sql"></param>
        ///// <param name="value"></param>
        ///// <returns></returns>
        //public virtual async Task<List<Entity>> ExecEntityProcedureAsync(string sql, object value)
        //{
        //    using (IDbConnection dbConn = dbFactory.OpenDbConnection())
        //    {
        //        return await dbConn.SqlListAsync<Entity>(sql, value);
        //    }

        //}

        ///// <summary>
        ///// return current Entity list using procedure 
        ///// </summary>
        ///// <param name="sql"></param>
        ///// <param name="value"></param>
        ///// <returns></returns>
        //public virtual async Task<List<Entity>> ExecEntityProcedureAsync(string sql)
        //{
        //    using (IDbConnection dbConn = dbFactory.OpenDbConnection())
        //    {
        //        return await dbConn.SqlListAsync<Entity>(sql);
        //    }
        //}

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

        ///// <summary>
        ///// return int result.
        ///// </summary>
        ///// <param name="sql"></param>
        ///// <param name="value"></param>
        ///// <returns></returns>
        //public virtual async Task<List<int>> ExecintProcedureAsync(string sql)
        //{
        //    using (IDbConnection dbConn = dbFactory.OpenDbConnection())
        //    {
        //        return await dbConn.SqlListAsync<int>(sql);
        //    }
        //}

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


        ///// <summary>
        ///// Return single entity data by expression
        ///// </summary>
        ///// <param name="Predicate"></param>
        ///// <returns></returns>
        ///// 
        //public virtual async Task<Entity> GetSingleDataAsync(Expression<Func<Entity, bool>> Predicate)
        //{
        //    using (IDbConnection dbConn = dbFactory.OpenDbConnection())
        //    {                
        //        return await dbConn.SingleAsync<Entity>(Predicate);
        //    }
        //}

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

        //public virtual async Task<List<Entity>> GetAllDataAsync()
        //{
        //    using (IDbConnection dbConn = dbFactory.OpenDbConnection())
        //    {
        //        return await dbConn.SelectAsync<Entity>();
        //    }
        //}

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

        ///// <summary>
        ///// get data by paging with where condition
        ///// </summary>
        ///// <param name="orderbyPredicate">order by field</param>
        ///// <param name="pageSize">page size</param>
        ///// <param name="pageNumber">page number, start from 1.</param>
        ///// <param name="isDesc">desc/asc</param>
        ///// <returns></returns>
        //public virtual async Task<List<Entity>> GetPagedDataByFuncAsync(Expression<Func<Entity, bool>> wherePredicate, Expression<Func<Entity, object>> orderbyPredicate, int pageSize, int pageNumber, bool? isDesc = null)
        //{
        //    using (IDbConnection dbConn = dbFactory.OpenDbConnection())
        //    {

        //        //var orderByStr = string.Format("{0} {1}", orderByField, (isDesc.HasValue && isDesc.Value == false) ? "desc" : "asc");
        //        var dataList = new List<Entity>();

        //        if (isDesc != null && isDesc.Value == true)
        //            dataList = await dbConn.SelectAsync<Entity>(p => p.Where(wherePredicate).OrderByDescending(orderbyPredicate).Limit(skip: (pageNumber - 1) * pageSize, rows: pageSize));
        //        else
        //            dataList = await dbConn.SelectAsync<Entity>(p => p.Where(wherePredicate).OrderBy(orderbyPredicate).Limit(skip: (pageNumber - 1) * pageSize, rows: pageSize));

        //        this.TotalCount = dbConn.Count<Entity>(p => p.Where(wherePredicate));
        //        this.pageSize = pageSize;

        //        return dataList;
        //    }
        //}


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

        ///// <summary>
        ///// get data by paging without where condition
        ///// </summary>
        ///// <param name="orderbyPredicate">order by field</param>
        ///// <param name="pageSize">page size</param>
        ///// <param name="pageNumber">page number, start from 1.</param>
        ///// <param name="isDesc">desc/asc</param>
        ///// <returns></returns>
        //public virtual async Task<List<Entity>> GetPagedDataByFuncAsync(Expression<Func<Entity, object>> orderbyPredicate, int pageSize, int pageNumber, bool? isDesc = null)
        //{
        //    using (IDbConnection dbConn = dbFactory.OpenDbConnection())
        //    {

        //        // var orderByStr = string.Format("{0} {1}", orderByField, (isDesc.HasValue && isDesc.Value == true) ? "desc" : "asc");
        //        var dataList = new List<Entity>();

        //        if (isDesc != null && isDesc.Value == true)
        //            dataList = await dbConn.SelectAsync<Entity>(p => p.OrderByDescending(orderbyPredicate).Limit(skip: (pageNumber - 1) * pageSize, rows: pageSize));
        //        else
        //            dataList = await dbConn.SelectAsync<Entity>(p => p.OrderBy(orderbyPredicate).Limit(skip: (pageNumber - 1) * pageSize, rows: pageSize));

        //        this.TotalCount = dbConn.Count<Entity>();
        //        this.pageSize = pageSize;

        //        return dataList;
        //    }
        //}

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
