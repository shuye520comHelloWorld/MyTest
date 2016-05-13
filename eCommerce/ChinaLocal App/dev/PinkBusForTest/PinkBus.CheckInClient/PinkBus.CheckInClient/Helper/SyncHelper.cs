using Newtonsoft.Json;
using PinkBus.CheckInClient.DAL;
using PinkBus.CheckInClient.Entitys;
using RestSharp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PinkBus.CheckInClient.Helper
{
    public class SyncHelper
    {
        public static Guid DownloadEvents(BackgroundWorker worker, string passcode)
        {
            try
            {
                var client = new RestClient(AppSetting.SyncDataAPI);
                var request = new RestRequest("/client/Checktoken/{token}", Method.GET);
                request.AddUrlSegment("token", passcode);
                request.AddParameter("TokenType", "1");
                request.AddHeader("Cache-Control", "no-cache");
                request.Timeout = 60000;
                IRestResponse res = client.Execute(request);
                CheckTokenResponse response = JsonConvert.DeserializeObject<CheckTokenResponse>(res.Content);
                
                if (response != null && response.ResponseStatus == null)
                {
                    worker.ReportProgress(5);
                    if (!EventDAL.GetSyncStatusLog(response.Event.EventKey, SyncType.D).Event)
                    {
                        EventDAL.InsertEvent(worker, response);
                    }

                    if (!EventDAL.GetSyncStatusLog(response.Event.EventKey, SyncType.D).Session)
                    {
                        EventDAL.InsertSessions(worker, response);
                    }

                    return response.Event.EventKey;
                }
                return Guid.Empty;
            }
            catch (Exception ex)
            {
                if (!Common.IsConnected())
                {
                    MessageBox.Show("当前客户端没有网络连接，请连接后重试！");
                }

                return Guid.Empty;
            }
        }

        public static void DownloadConsultantInfo(BackgroundWorker worker, Guid eventKey, int type)
        {
            try
            {
                var client = new RestClient(AppSetting.SyncDataAPI);
                var request = new RestRequest("/client/downloaddata/{EventKey}", Method.GET);
                request.AddUrlSegment("EventKey", eventKey.ToString());
                request.AddParameter("SyncDataType", type);
                request.AddHeader("Cache-Control", "no-cache");
                request.Timeout = 60000;
                IRestResponse response = client.Execute(request);
                //worker.ReportProgress(10);
                DownloadDataResponse data = JsonConvert.DeserializeObject<DownloadDataResponse>(response.Content);

                string json = Common.Decompress(data.data);

                if (data != null && data.ResponseStatus == null)
                {
                    if (type == 1)
                    {
                        worker.ReportProgress(15);
                        List<Consultant> resp = JsonConvert.DeserializeObject<List<Consultant>>(json);
                        EventDAL.InsertConsultants(worker, resp, eventKey);

                    }
                    else if (type == 2)
                    {
                        worker.ReportProgress(45);
                        List<Ticket> resp = JsonConvert.DeserializeObject<List<Ticket>>(json);
                        EventDAL.InsertTickets(worker, resp, eventKey);
                    }
                    else if (type == 3)
                    {
                        worker.ReportProgress(75);
                        List<Customer> resp = JsonConvert.DeserializeObject<List<Customer>>(json);
                        EventDAL.InsertCustomers(worker, resp, eventKey);
                    }
                }

                //return null;
            }
            catch (Exception ex)
            {
                if (!Common.IsConnected())
                {
                    MessageBox.Show("当前客户端没有网络连接，请连接后重试！");
                }
                // return null;
            }
        }

        public static void DownloadSellerHeaders(BackgroundWorker worker, Guid eventKey)
        {
            try
            {
                List<object> sellers = EventDAL.QuerySellersContractId(eventKey);
                var authorization = "Basic " + Common.EncodeByBase64(AppSetting.ClientKey + ":" + AppSetting.ClientSecret);

                var client = new RestClient(AppSetting.OauthTokenUrl);
                var request = new RestRequest(Method.POST);
                request.AddParameter("grant_type", "client_credentials");
                request.AddHeader("Authorization", authorization);
                request.Timeout = 60000;
                IRestResponse<OauthTokenModel> response = client.Execute<OauthTokenModel>(request);

                if (!string.IsNullOrEmpty(response.Data.access_token))
                {
                    var ImgClient = new RestClient(AppSetting.ConsultantImageAPI);

                    sellers.ForEach(m =>
                    {
                        try
                        {
                            var imgRequest = new RestRequest(Method.GET);
                            imgRequest.AddParameter("contact_id", m);
                            imgRequest.AddParameter("access_token", response.Data.access_token);
                            IRestResponse<ConsultantImageModel> imgResponse = ImgClient.Execute<ConsultantImageModel>(imgRequest);

                            if (!string.IsNullOrEmpty(imgResponse.Data.image_data))
                            {
                                var headerFile = m + ".jpg";
                                Common.Base64StringToImage(imgResponse.Data.image_data, savedImageFileName: headerFile);
                            }
                        }
                        catch (Exception ex)
                        {

                        }
                    });

                }

                string sql = "update SyncStatusLog set HeaderImgs=1,Complete=1 where eventkey='" + eventKey.ToString().ToLower() + "' and SyncType='D'";
                EventDAL.OperationSqlite(e => e.Execute(sql));
                worker.ReportProgress(100);
            }
            catch (Exception ex)
            {
                if (!Common.IsConnected())
                {
                    MessageBox.Show("当前客户端没有网络连接，请连接后重试！");
                }

            }
            //var OauthToken = ServiceHttpRequest.Post<OauthTokenModel>(UtilityHelper.OauthTokenUrl, httpParams, headerParams, false);

        }

        public static Guid UploadCheckEvent(string passcode)
        {
            try
            {
                var client = new RestClient(AppSetting.SyncDataAPI);
                var request = new RestRequest("/client/Checktoken/{token}", Method.GET);
                request.AddUrlSegment("token", passcode);
                request.AddParameter("TokenType", "2");
                request.AddHeader("Cache-Control", "no-cache");
                request.Timeout = 60000;
                IRestResponse res = client.Execute(request);
                CheckTokenResponse response = JsonConvert.DeserializeObject<CheckTokenResponse>(res.Content);

                if (response != null && response.ResponseStatus == null)
                {
                    List<Event> events = EventDAL.QueryEvents(" where eventkey='" + response.Event.EventKey + "' ");
                    if (events.Count > 0)
                    {
                        return response.Event.EventKey;
                    }
                }
                return Guid.Empty;
            }
            catch (Exception ex)
            {
                if (!Common.IsConnected())
                {
                    MessageBox.Show("当前客户端没有网络连接，请连接后重试！");
                }

                return Guid.Empty;
            }
        }


        public static bool UploadConsultant(Guid eventKey, Consultant con,out string contactId)
        {
            // Consultant con=EventDAL.GetUploadConsultantTop1();
            var res = UploadData(eventKey, JsonConvert.SerializeObject(con), 1);

            string sql = "update PB_Event_Consultant set ChangeStatus='" + (res.Result ? 0 : 1) + "',SyncTimes=SyncTimes+1,SyncMsg='" + (res.ResponseStatus==null?"":res.ResponseStatus.Message) + "' where MappingKey='" + con.MappingKey + "'";
            EventDAL.ExecuteNonQuerySql(sql);
            contactId = res.ContactId;
            return res.Result;
        }


        public static bool UploadCustomer(Guid eventKey, Customer cus)
        {
            var res = UploadData(eventKey, JsonConvert.SerializeObject(cus), 3);
            if (res.Result)
            {
                string sql = "update PB_Customer set ChangeStatus=0,SyncTimes=SyncTimes+1,SyncMsg='" + (res.ResponseStatus == null ? "" : res.ResponseStatus.Message) + "' where CustomerKey='" + cus.CustomerKey + "'";
                if (EventDAL.ExecuteNonQuerySql(sql) > 0)
                {
                    return true;
                }
            }
            return res.Result;
        }

        public static bool UploadTicket(Guid eventKey, Ticket tic)
        {
            var res = UploadData(eventKey, JsonConvert.SerializeObject(tic), 2);
            if (res.Result)
            {
                string sql = "update PB_Ticket set ChangeStatus=0,SyncTimes=SyncTimes+1,SyncMsg='" + (res.ResponseStatus == null ? "" : res.ResponseStatus.Message) + "' where TicketKey='" + tic.TicketKey + "'";
                if (EventDAL.ExecuteNonQuerySql(sql) > 0)
                {
                    return true;
                }
            }
            return res.Result;
        }

        public static bool UploadCheckinVolunteer(Guid eventKey, VolunteerCheckin vol)
        {
            var res = UploadData(eventKey, JsonConvert.SerializeObject(vol), 4);
            if (res.Result)
            {
                string sql = "update PB_VolunteerCheckin set ChangeStatus=0,SyncTimes=SyncTimes+1,SyncMsg='" + (res.ResponseStatus == null ? "" : res.ResponseStatus.Message) + "' where Key='" + vol.Key + "'";
                if (EventDAL.ExecuteNonQuerySql(sql) > 0)
                {
                    return true;
                }
            }
            return res.Result;
        }


        private static UploadDataResponse UploadData(Guid eventKey, string jsonData, int SyncDataType)
        {

            var client = new RestClient(AppSetting.SyncDataAPI);
            var request = new RestRequest("/client/uploadcheckindata/{EventKey}", Method.POST);
            request.AddUrlSegment("EventKey", eventKey.ToString());
            request.AddParameter("SyncDataType", SyncDataType);
            request.AddParameter("UploadJsonData", jsonData);
            request.AddHeader("Cache-Control", "no-cache");
            request.Timeout = 60000;
            IRestResponse response = client.Execute(request);
            UploadDataResponse data = JsonConvert.DeserializeObject<UploadDataResponse>(response.Content);

            return data;
        }

    }
}
