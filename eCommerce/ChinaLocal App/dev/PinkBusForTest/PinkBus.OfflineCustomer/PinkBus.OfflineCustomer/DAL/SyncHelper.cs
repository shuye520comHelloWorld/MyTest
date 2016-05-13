using Newtonsoft.Json;
using PinkBus.OfflineCustomer.Entity;
using RestSharp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PinkBus.OfflineCustomer.Common;
using System.Windows.Forms;
using System.Data;

namespace PinkBus.OfflineCustomer.DAL
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
                worker.ReportProgress(5);
                if (response != null && response.ResponseStatus == null)
                {
                    if (!OfflineCustomerHelper.GetSyncStatusLog(response.Event.EventKey, SyncType.D).Event)
                    {
                        OfflineCustomerHelper.InsertEvent(worker, response);
                    }
                    if (!OfflineCustomerHelper.GetSyncStatusLog(response.Event.EventKey, SyncType.D).Session)
                    {
                        OfflineCustomerHelper.InsertSessions(worker, response);
                    }

                    return response.Event.EventKey;
                }
                return Guid.Empty;
            }
            catch (Exception ex)
            {
                if (!ConnectStatus.IsConnected())
                {
                    MessageBox.Show("当前客户端没有网络连接，请连接后重试！");
                }
                else
                    MessageBox.Show(ex.Message);
                return Guid.Empty;
            }
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
                if (response == null)
                    return Guid.Empty;
                return response.Event.EventKey;               
            }
            catch (Exception ex)
            {
                if (!ConnectStatus.IsConnected())
                {
                    MessageBox.Show("当前客户端没有网络连接，请连接后重试！");
                }

                return Guid.Empty;
            }
        }


        public static bool UploadCustomer(Guid eventKey, Customer cus)
        {
            var res = UploadData(eventKey, JsonConvert.SerializeObject(cus), 3);
            if (res.Result)
            {
                string sql = "update PB_Customer set State=2,SyncTimes=SyncTimes+1,SyncMsg='" + res.Message + "' where CustomerKey='" + cus.CustomerKey + "'";
                if (OfflineCustomerHelper.ExecuteNonQuerySql(sql) > 0)
                {
                    return true;
                }
             
            }
            else
                MessageBox.Show("上传失败," + res.Message);
            return res.Result;
        }

        private static UploadDataResponse UploadData(Guid eventKey, string jsonData, int SyncDataType)
        {

            var client = new RestClient(AppSetting.SyncDataAPI);
            var request = new RestRequest("/client/offlinecustomer/{EventKey}", Method.POST);
            request.AddUrlSegment("EventKey", eventKey.ToString());
           // request.AddUrlSegment("EventKey", eventKey.ToString());       
            request.AddParameter("JsonData", jsonData);
            request.AddHeader("Cache-Control", "no-cache");
            request.Timeout = 60000;
            IRestResponse response = client.Execute(request);
            UploadDataResponse data = JsonConvert.DeserializeObject<UploadDataResponse>(response.Content);

            return data;
        }

     

        //public static void DownloadSellerHeaders(BackgroundWorker worker, Guid EventKey)
        //{
        //    try
        //    {
        //        List<object> sellers = OfflineCustomerHelper.QuerySellersContractId(EventKey);
        //        var authorization = "Basic " + Common.EncodeByBase64(AppSetting.ClientKey + ":" + AppSetting.ClientSecret);

        //        var Client = new RestClient(AppSetting.OauthTokenUrl);
        //        var request = new RestRequest(Method.POST);
        //        request.AddParameter("grant_type", "client_credentials");
        //        request.AddHeader("Authorization", authorization);
        //        request.Timeout = 60000;
        //        IRestResponse<OauthTokenModel> response = Client.Execute<OauthTokenModel>(request);

        //        if (!string.IsNullOrEmpty(response.Data.access_token))
        //        {
        //            var ImgClient = new RestClient(AppSetting.ConsultantImageAPI);

        //            sellers.ForEach(m =>
        //            {
        //                try
        //                {
        //                    var ImgRequest = new RestRequest(Method.GET);
        //                    ImgRequest.AddParameter("contact_id", m);
        //                    ImgRequest.AddParameter("access_token", response.Data.access_token);
        //                    IRestResponse<ConsultantImageModel> ImgResponse = ImgClient.Execute<ConsultantImageModel>(ImgRequest);

        //                    if (!string.IsNullOrEmpty(ImgResponse.Data.image_data))
        //                    {
        //                        var headerFile = m + ".jpg";
        //                        CommonHelper.Base64StringToImage(ImgResponse.Data.image_data, savedImageFileName: headerFile);
        //                    }
        //                }
        //                catch (Exception ex)
        //                {

        //                }
        //            });

        //        }

        //        string sql = "update SyncStatusLog set HeaderImgs=1,Complete=1 where eventkey='" + EventKey.ToString().ToLower() + "' and SyncType='D'";
        //        OfflineCustomerHelper.OperationSqlite(e => e.Execute(sql));
        //        worker.ReportProgress(100);
        //    }
        //    catch (Exception ex)
        //    {
        //        if (!Common.IsConnected())
        //        {
        //            MessageBox.Show("当前客户端没有网络连接，请连接后重试！");
        //        }

        //    }
        //    //var OauthToken = ServiceHttpRequest.Post<OauthTokenModel>(UtilityHelper.OauthTokenUrl, httpParams, headerParams, false);

        //}
    }
}
