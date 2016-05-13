using NLog;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace IParty.SyncOrdersAndLevel.WindowsService
{
    public class SyncOrdersAndLevel
    {
        private Logger _logger = LogManager.GetCurrentClassLogger();
        private DateTime _nextRunTime;
        private DateTime _currentRunTime;
        public SyncOrdersAndLevel()
        {
            _currentRunTime = DateTime.Now;
            _nextRunTime = _currentRunTime.Date.AddDays(1);
        }

        public async Task SyncOrdersAndLevelTask()
        {
            var eventKeys = ExecutionBlock(() => GetActiveEventKeys());

            // if no event, exit
            if (eventKeys == null || !eventKeys.Any())
                return;

            while (true)
            {
                // if no customer under the events, exit
                var customerDt = ExecutionBlock(() => GetCustomers(eventKeys));
                if (customerDt == null)
                    return;

                var customerList = customerDt.AsEnumerable();
                // guest who doesn't have MK contact id
                #region Guests

                var guests = customerList.Where(r => r["ContactId"] == null || string.IsNullOrEmpty(r["ContactId"].ToString())); // contactid is empty
                if (guests != null && guests.Any())
                {
                    foreach (var guest in guests)
                    {
                        ExecutionBlock(() =>
                        {
                            // get contact info via phone number, if the customer joined MK, the result will not be null
                            var newContactInfo = GetConsultantByPhone(guest["PhoneNumber"].ToString());
                            if (newContactInfo == null)
                            {
                                SaveCustomer(
                                    guest["CustomerKey"].ToString(),
                                    string.Format("NextProcessTime = '{0}'",
                                    _nextRunTime.ToString("yyyy-MM-dd HH:mm:ss")));
                                return;
                            }

                            // check if the customer joined the party hosts' units (including main host and unitee hosts)
                            if (IsNewer(guest["PartyKey"].ToString(), newContactInfo["DirContactId"].ToString()))
                            {

                                var hasOrder = CheckIfHasOrder(newContactInfo["ContactId"].ToString());

                                //guest["IsNewer"] = 1;  // set the customer is newer
                                //guest["ContactId"] = newContactInfo["ContactID"];
                                SaveCustomer(
                                    guest["CustomerKey"].ToString(),
                                    string.Format("CustomerContactId = '{0}', IsNewer = {1}, HasOrder = {2}, NextProcessTime = '{3}'",
                                    newContactInfo["ContactID"].ToString(),
                                    1,
                                    hasOrder ? 1 : 0,
                                    _nextRunTime.ToString("yyyy-MM-dd HH:mm:ss")));
                            }
                            else
                            {
                                SaveCustomer(
                                    guest["CustomerKey"].ToString(),
                                    string.Format("NextProcessTime = '{0}'",
                                    _nextRunTime.ToString("yyyy-MM-dd HH:mm:ss")));
                            }
                        });
                    }
                }

                #endregion

                #region Members
                var members = customerList.Where(r => r["ContactId"] != null && !string.IsNullOrEmpty(r["ContactId"].ToString()));
                if (members != null && members.Any())
                {
                    foreach (var member in members)
                    {
                        ExecutionBlock(() =>
                        {
                            var nowMemberInfo =
                                GetConsultantByContactId(member["ContactId"].ToString());

                            if (nowMemberInfo == null)
                                throw new Exception("Failed to find contact id " + member["ContactId"].ToString());

                            var setCondition = string.Empty;

                            if (!bool.Parse(member["IsNewer"].ToString())
                                && !bool.Parse(member["IsPromoted"].ToString()))
                            {
                                var levelBefore = int.Parse(member["Level"].ToString());
                                var levelAfter = int.Parse(nowMemberInfo["ConsultantLevelID"].ToString());
                                var toPromote = IsPromotion(levelBefore, levelAfter);

                                if (toPromote)
                                {
                                    setCondition += string.Format("IsPromoted = {0}", 1);
                                }
                            }

                            if (!bool.Parse(member["HasOrder"].ToString()))
                            {
                                var hasOrder = CheckIfHasOrder(member["ContactId"].ToString());
                                if (hasOrder)
                                {
                                    setCondition = string.IsNullOrEmpty(setCondition)
                                        ? string.Format("HasOrder = {0}", 1)
                                        : setCondition + ", " + string.Format("HasOrder = {0}", 1);
                                }
                            }

                            if (!string.IsNullOrEmpty(setCondition))
                            {
                                setCondition += string.Format(", NextProcessTime = '{0}'", _nextRunTime.ToString("yyyy-MM-dd HH:mm:ss"));
                            }
                            else
                            {
                                setCondition = string.Format("NextProcessTime = '{0}'",
                                    _nextRunTime.ToString("yyyy-MM-dd HH:mm:ss"));
                            }

                            SaveCustomer(
                                    member["CustomerKey"].ToString(),
                                    setCondition);
                        });
                    }
                }

                #endregion
            }

            _logger.Info(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "__all done");
        }

        private List<Guid> GetActiveEventKeys()
        {
            var sql = @"SELECT * FROM iParty_Event WITH (NOLOCK)
                        WHERE CONVERT(CHAR(10), '{0}', 120)
                            BETWEEN CONVERT(CHAR(10), EventStartDate, 120)  
                            AND CONVERT(CHAR(10), EventEndDate, 120)
                        AND CONVERT(CHAR(10), '{1}', 120)
                            BETWEEN CONVERT(CHAR(10), EventStartDate, 120)  
                            AND CONVERT(CHAR(10), EventEndDate, 120)";

            var ds = SqlHelper.ExecuteDataset(ConnectionStrings.Community, CommandType.Text, string.Format(sql, AppConfigs.StartDate, AppConfigs.EndDate));

            var result = new List<Guid>();
            if (ds != null && ds.Tables != null && ds.Tables.Count > 0)
            {
                DataTable dt = ds.Tables[0];
                foreach (DataRow row in dt.Rows)
                {
                    result.Add(Guid.Parse(row["EventKey"].ToString()));
                }
            }

            return result;
        }

        private DataTable GetCustomers(IEnumerable<Guid> eventKeys)
        {
            var sql = @"SELECT TOP {0}
                          c.CustomerKey
                        , c.CustomerContactId AS ContactId
                        , i.PartyKey
                        , c.PhoneNumber
                        , c.Level
                        , c.HasOrder
                        , c.IsNewer
                        , c.IsPromoted
                        FROM IParty_Customer c WITH (NOLOCK)
                        INNER JOIN iParty_Invitation i WITH (NOLOCK)
                        ON c.CustomerKey = i.CustomerKey
                        INNER JOIN iParty_PartyApplication p WITH (NOLOCK)
                        ON i.PartyKey = p.PartyKey
                        WHERE p.EventKey IN ({1}) AND (c.NextProcessTime IS NULL OR c.NextProcessTime <= '{2}')
                        AND ((c.CustomerContactId IS NOT NULL AND c.IsNewer <> 1 AND c.IsPromoted <> 1)
                        OR c.CustomerContactId IS NULL
                        OR (c.CustomerContactId IS NOT NULL AND c.HasOrder <> 1))";

            var eventKeysString = String.Join(",", eventKeys.Select(guid => "'" + guid.ToString() + "'"));
            var ds = SqlHelper.ExecuteDataset(
                ConnectionStrings.Community,
                CommandType.Text,
                string.Format(
                    sql,
                    AppConfigs.PageCount,
                    eventKeysString,
                    _currentRunTime.ToString("yyyy-MM-dd HH:mm:ss")));

            if (ds != null && ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count != 0)
            {
                return ds.Tables[0];
            }
            else
            {
                return null;
            }
        }

        private DataRow GetConsultantByPhone(string phoneNumber)
        {
            var ds = SqlHelper.ExecuteDataset(
                ConnectionStrings.ContactsLite,
                CommandType.StoredProcedure,
                "Usp_GetConsultantInfoByPhone",
                new SqlParameter("@PhoneNumber", phoneNumber));

            if (ds != null && ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count != 0)
            {
                DataTable dt = ds.Tables[0];
                if (ds.Tables[0].Rows.Count != 1)
                    throw new Exception("Get more than one consultant info via phone number" + phoneNumber);

                return dt.AsEnumerable().FirstOrDefault();
            }
            else
            {
                return null;
            }
        }

        private DataRow GetConsultantByContactId(string contactId)
        {
            var sql = @"SELECT [ContactID]
                          ,[ConsultantLevelID] 
                      FROM [dbo].[Consultants] WITH (NOLOCK)
                      WHERE ContactID = {0}";
            var ds = SqlHelper.ExecuteDataset(
                ConnectionStrings.ContactsLite,
                CommandType.Text,
                string.Format(sql, contactId));

            if (ds != null && ds.Tables != null && ds.Tables.Count > 0)
            {
                DataTable dt = ds.Tables[0];

                return dt.AsEnumerable().FirstOrDefault();
            }
            else
            {
                return null;
            }
        }

        private Dictionary<string, List<string>> _dirContactDic =
            new Dictionary<string, List<string>>();
        private List<string> GetApplicationHostContactIds(string partyKey)
        {
            if (_dirContactDic.ContainsKey(partyKey))
                return _dirContactDic[partyKey];

            var sql = @"SELECT AppliedContactID AS ContactID FROM iParty_PartyApplication WITH (NOLOCK)
                        WHERE PartyKey = '{0}'
                        UNION ALL
                        select UniteeContactID AS ContactID from iParty_Unitee WITH (NOLOCK)
                        WHERE PartyKey = '{0}'";

            var hostDs = SqlHelper.ExecuteDataset(
                ConnectionStrings.Community,
                CommandType.Text,
                string.Format(sql, partyKey));

            var dirContactIds = new List<string>();
            if (hostDs != null && hostDs.Tables != null && hostDs.Tables.Count > 0 && hostDs.Tables[0].Rows.Count != 0)
            {
                DataTable dt = hostDs.Tables[0];
                foreach (DataRow row in dt.Rows)
                {
                    var dirContactId = row["ContactID"].ToString();
                    if (!dirContactIds.Contains(dirContactId))
                        dirContactIds.Add(dirContactId);
                }
            }

            _dirContactDic.Add(partyKey, dirContactIds);
            return dirContactIds;
        }

        private bool IsNewer(string partyKey, string dirContactIdOfNewer)
        {
            var directContactIds = GetApplicationHostContactIds(partyKey);
            return directContactIds.Contains(dirContactIdOfNewer);
        }

        private bool CheckIfHasOrder(string contactId)
        {
            var sql = @"SELECT *
                        FROM dbo.btc_OrderFromOE o WITH (NOLOCK)
                        WHERE o.PurchaserContactID = {0}
                        AND o.OrderDate BETWEEN '{1}' AND '{2}'";
            var orderDs = SqlHelper.ExecuteDataset(
                ConnectionStrings.Community,
                CommandType.Text,
                string.Format(sql, contactId, AppConfigs.StartDate, AppConfigs.EndDate));

            if (orderDs != null && orderDs.Tables != null && orderDs.Tables.Count > 0)
            {
                DataTable dt = orderDs.Tables[0];

                return dt.Rows.Count > 0;
            }
            else
            {
                return false;
            }
        }

        private bool IsPromotion(int levelBefore, int levelAfter)
        {
            if (levelBefore >= levelAfter || levelAfter < 15)
                return false;

            return true;
        }

        private void SaveCustomer(string customerKey, string setCondition)
        {
            var sql = @"UPDATE IParty_Customer
                        SET {0}
                        WHERE CustomerKey = '{1}'";

            var result = SqlHelper.ExecuteNonQuery(
                ConnectionStrings.Community,
                CommandType.Text,
                string.Format(sql, setCondition, customerKey));

            if (result == -1)
                throw new Exception(
                    string.Format("Failed to update customer info {0} \"{1}\" into table",
                    customerKey,
                    setCondition));
        }

        private void LogError(string errorMessage, string exceptionStack)
        {
            _logger.Error(errorMessage + Environment.NewLine + exceptionStack);
        }

        private void LogInfo(string errorMessage)
        {
            _logger.Info(errorMessage);
        }

        private void ExecutionBlock(Action action)
        {
            int retry = 3;
            while (retry > 0)
            {
                try
                {
                    action();
                    return;
                }
                catch (Exception ex)
                {
                    retry--;
                    if (retry == 0)
                        LogError(ex.Message, ex.StackTrace);
                    else
                        LogInfo(ex.Message);
                }
            }
        }

        private T ExecutionBlock<T>(Func<T> func)
        {
            int retry = 2;
            while (retry > 0)
            {
                try
                {
                    return func();
                }
                catch (Exception ex)
                {
                    retry--;
                    if (retry == 0)
                    {
                        LogError(ex.Message, ex.StackTrace);
                    }
                    else
                    {
                        LogInfo(ex.Message);
                    }
                }
            }

            return default(T);
        }
    }
}
