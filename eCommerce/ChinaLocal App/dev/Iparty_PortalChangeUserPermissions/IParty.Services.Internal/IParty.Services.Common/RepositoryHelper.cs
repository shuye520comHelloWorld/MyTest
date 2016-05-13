using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace IParty.Services.Common
{
    public class RepositoryHelper
    {
        public static IList<T> Reader<T>(string connString, string spName, params object[] parameters)
        {
            SqlDataReader reader = SqlHelper.ExecuteReader(connString, spName, parameters);

            IList<T> result = new List<T>();
            var builder = ObjectBuilderFromReader.Build<T>();
            using (reader)
            {
                while (reader.Read())
                {
                    result.Add(builder.Invoke(reader));
                }
            }

            return result;
        }


        public static IList<T> Query<T>(string connString, string spName, params SqlParameter[] commandParameters)
        {
            SqlDataReader reader = SqlHelper.ExecuteReaderPage(connString, spName, commandParameters);

            IList<T> result = new List<T>();
            var builder = ObjectBuilderFromReader.Build<T>();
            using (reader)
            {
                while (reader.Read())
                {
                    result.Add(builder.Invoke(reader));
                }
            }

            return result;
        }

        public static T Single<T>(string connString, string spName, params object[] parameters)
        {
            SqlDataReader reader = SqlHelper.ExecuteReader(connString, spName, parameters);
            T result;
            using (reader)
            {
                if (reader.Read())
                {

                    result = ObjectBuilderFromReader.Build<T>().Invoke(reader);
                }
                else
                {
                    result = default(T);
                }
            }
            return result;

        }
        /// <summary>
        /// Read the specified table
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="connString"></param>
        /// <param name="spName"></param>
        /// <param name="tableNum">start from zero</param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public static IList<T> ReaderByTableNum<T>(string connString, string spName, int tableNum, params object[] parameters)
        {
            SqlDataReader reader = SqlHelper.ExecuteReader(connString, spName, parameters);

            IList<T> result = new List<T>();

            try
            {
                for (int i = 0; i <= tableNum; i++)
                {
                    if (i == tableNum)
                    {
                        using (reader)
                        {
                            while (reader.Read())
                            {
                                result.Add(ObjectBuilderFromReader.Build<T>().Invoke(reader));
                            }
                        }

                        break;
                    }
                    reader.NextResult();
                }
            }
            finally
            {
            }

            return result;
        }

        /// <summary>
        /// ExecuteNonQuery with return value
        /// </summary>
        /// <param name="connString"></param>
        /// <param name="spName"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public static int Set(string connString, string spName, params object[] parameters)
        {
            int nResult = SqlHelper.ExecuteNonQuery(connString, spName, parameters);
            return nResult;
        }

        /// <summary>
        /// ExecuteNonQuery without return value
        /// </summary>
        /// <param name="connString"></param>
        /// <param name="spName"></param>
        /// <param name="parameters"></param>
        public static void Execute(string connString, string spName, params object[] parameters)
        {
            int nResult = SqlHelper.ExecuteNonQuery(connString, spName, parameters);
        }
        /// <summary>
        /// ExecuteNonQuery with transation
        /// </summary>
        /// <param name="transaction"></param>
        /// <param name="spName"></param>
        /// <param name="parameters"></param>
        public static void Execute(SqlTransaction transaction, string spName, params SqlParameter[] parameters)
        {
            int nResult = SqlHelper.ExecuteNonQuery(transaction, System.Data.CommandType.StoredProcedure, spName, parameters);
        }

        public static void Execute(string connString, string spName, params SqlParameter[] parameters)
        {
            int nResult = SqlHelper.ExecuteNonQuery(connString, System.Data.CommandType.StoredProcedure, spName, parameters);
        }

        public static object ExecuteScalar(string connString, string spName, params object[] parameters)
        {
            return SqlHelper.ExecuteScalar(connString, spName, parameters);
        }

        public static SqlDataReader ExecuteReader(string connectionstring, string spName, params object[] parameters)
        {
            return SqlHelper.ExecuteReader(connectionstring, spName, parameters);
        }

        /// <summary>
        /// ExecuteDataset without return value
        /// </summary>
        public static DataSet ExecuteDataset(string connString, string spName, params SqlParameter[] parameters)
        {
            return SqlHelper.ExecuteDataset(connString, System.Data.CommandType.StoredProcedure, spName, parameters);
        }

        public static DataSet ExecuteDataset(string connString, string spName, params object[] parameters)
        {
            return SqlHelper.ExecuteDataset(connString, spName, parameters);
        }
    }
}