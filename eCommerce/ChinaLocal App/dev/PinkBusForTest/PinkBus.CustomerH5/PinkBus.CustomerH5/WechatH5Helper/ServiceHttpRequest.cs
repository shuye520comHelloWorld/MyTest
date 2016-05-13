
using Newtonsoft.Json;
using NLog;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace WechatH5Helper
{
    public static class ServiceHttpRequest
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        public static T OperateRequest<T>(string methodType, string url, Dictionary<string, string> httpParams)
        {

            try
            {
                if (methodType == MethodType.GET.ToString() && httpParams.Count > 0)
                {
                    var formDataString = FormatParams(httpParams);
                    url += "?" + formDataString;

                }
                LogHelper.Debug("begin request, url:" + url);
                var req = WebRequest.Create(url) as HttpWebRequest;
                req.Method = methodType;
                req.ContentType = "application/x-www-form-urlencoded";

                if (methodType == MethodType.POST.ToString())
                {
                    httpParams.Add("CommandId", Guid.NewGuid().ToString());
                    using (var rs = req.GetRequestStream())
                    {
                        var formDataString = FormatParams(httpParams);
                        var formData = Encoding.UTF8.GetBytes(formDataString);
                        rs.Write(formData, 0, formData.Length);
                    }
                }
                var response = req.GetResponse();
                var result = string.Empty;
                using (var receivedStream = response.GetResponseStream())
                {
                    var streamReader = new StreamReader(receivedStream);
                    result = streamReader.ReadToEnd();
                }
                response.Close();
                return JsonOperate.JsonToObject<T>(result);
            }
            catch (WebException ex)
            {
                var rr = ex.Status;
                string par = "";
                foreach (var item in httpParams)
                {
                    par += item.Key + ":" + item.Value + "   ";
                }

                //if ((!url.ToLower().Contains("user/unionid")) 
                //    || (!url.Contains("OAuthServer/OAuth/Token"))
                //    || (!url.Contains("wechat/authorization-code/login")))
                //{
                //    logger.Error("request exception message:" + ex.Message + "\r\n URL:" + url + "\r\n par:" + par + "\r\n StackTrace:" + ex.StackTrace);
                //}

                logger.Info("in ServiceHttpRequest,url is: "+url);

                if (!(url.ToLower().Contains("user/unionid")
                        || url.Contains("OAuthServer/OAuth/Token")
                        || url.Contains("wechat/authorization-code/login")))
                {
                    logger.Error("request exception message:" + ex.Message + "\r\n URL:" + url + "\r\n par:" + par + "\r\n StackTrace:" + ex.StackTrace);
                }

                if (url.Contains("OAuthServer/OAuth/Token"))
                {
                    logger.Info("request exception message:" + ex.Message + "\r\n URL:" + url + "\r\n par:" + par + "\r\n StackTrace:" + ex.StackTrace);
                }

                var result = string.Empty;
                using (var receivedStream = ex.Response.GetResponseStream())
                {
                    var streamReader = new StreamReader(receivedStream);
                    result = streamReader.ReadToEnd();
                }
                return JsonConvert.DeserializeObject<T>(result);
            }

        }

        public static T OperateRequest<T>(string methodType, string url, Dictionary<string, string> httpParams, Dictionary<string, string> hearderParams)
        {
            try
            {

                var req = WebRequest.Create(url) as HttpWebRequest;
                req.Method = methodType;
                req.ContentType = "application/x-www-form-urlencoded";

                if (hearderParams != null)
                {
                    foreach (KeyValuePair<string, string> item in hearderParams)
                    {
                        req.Headers.Add(item.Key, item.Value);
                    }
                }

                if (methodType == MethodType.POST.ToString())
                {
                    httpParams.Add("CommandId", Guid.NewGuid().ToString());
                    using (var rs = req.GetRequestStream())
                    {
                        var formDataString = FormatParams(httpParams);
                        var formData = Encoding.UTF8.GetBytes(formDataString);
                        rs.Write(formData, 0, formData.Length);
                    }
                }

                var response = req.GetResponse();
                var result = string.Empty;
                using (var receivedStream = response.GetResponseStream())
                {
                    var streamReader = new StreamReader(receivedStream);
                    result = streamReader.ReadToEnd();
                }
                response.Close();
                return JsonConvert.DeserializeObject<T>(result);
            }
            catch (WebException ex)
            {

                string par = "";
                foreach (var item in httpParams)
                {
                    par += item.Key + ":" + item.Value + "   ";
                }
                logger.Error("request exception message:" + ex.Message + "\r\n URL:" + url + "\r\n par:" + par + "\r\n StackTrace:" + ex.StackTrace);

                var result = string.Empty;
                using (var receivedStream = ex.Response.GetResponseStream())
                {
                    var streamReader = new StreamReader(receivedStream);
                    result = streamReader.ReadToEnd();
                }
                logger.Error(result);
                return JsonConvert.DeserializeObject<T>(result);

            }
        }




        public static string FormatParams(Dictionary<string, string> httpParams)
        {
            StringBuilder builder = new StringBuilder();
            foreach (KeyValuePair<string, string> kvp in httpParams)
            {
                builder.Append(kvp.Key + "=" + kvp.Value + "&");
            }
            return builder.ToString().Trim('&');
        }

        public static T Post<T>(string url, Dictionary<string, string> httpParams)
        {
            return OperateRequest<T>(MethodType.POST.ToString(), url, httpParams);
        }

        public static T Get<T>(string url, Dictionary<string, string> httpParams)
        {
            return OperateRequest<T>(MethodType.GET.ToString(), url, httpParams);
        }
        public static T Post<T>(string url, Dictionary<string, string> httpParams, Dictionary<string, string> hearderParams = null)
        {
            return OperateRequest<T>(MethodType.POST.ToString(), url, httpParams, hearderParams);
        }
        public static T Get<T>(string url, Dictionary<string, string> httpParams, Dictionary<string, string> hearderParams = null)
        {
            return OperateRequest<T>(MethodType.GET.ToString(), url, httpParams, hearderParams);
        }

        //public static T CommandPost<T>(string url, Dictionary<string, string> httpParams)
        //{
        //    try
        //    {
        //        Dictionary<string, string> CommandParams = new Dictionary<string, string>();
        //        CommandParams["OAuthKey"] = ConfigurationManager.AppSettings["OAuthKey"];
        //        CommandModel command = OperateRequest<CommandModel>(MethodType.POST.ToString(), GlobalAppSettings.BeautyContestTW_Command + "Command", CommandParams);

        //        if (command != null && !string.IsNullOrEmpty(command.CommandId))
        //        {
        //            httpParams.Add("CommandId", command.CommandId);
        //            return OperateRequest<T>(MethodType.POST.ToString(), url, httpParams);
        //        }
        //        else
        //        {
        //            return default(T);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        return default(T);
        //    }
        //}

    }

    public enum MethodType
    {
        POST,
        GET
    }
}
