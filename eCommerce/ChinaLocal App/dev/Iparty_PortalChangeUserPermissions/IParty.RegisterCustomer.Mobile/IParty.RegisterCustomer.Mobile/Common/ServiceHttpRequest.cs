
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Web;
using System.Net.Http;

namespace IParty.RegisterCustomer.Mobile.Common
{
    public static class ServiceHttpRequest
    {
        public static T OperateRequest<T>(string methodType, string url, Dictionary<string, string> httpParams)
        {
            try
            {
                if (methodType == MethodType.GET.ToString())
                {
                    url = url + "?" + FormatParams(httpParams);
                }
                var req = WebRequest.Create(url) as HttpWebRequest;
                req.Method = methodType;
                req.ContentType = "application/x-www-form-urlencoded";
               
                if (methodType == MethodType.POST.ToString())
                {
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
                LogHelper.Debug(result);
                return JsonConvert.DeserializeObject<T>(result);
            }catch(WebException ex){
                var result = string.Empty;
                using (var receivedStream = ex.Response.GetResponseStream())
                {
                    var streamReader = new StreamReader(receivedStream);
                     result = streamReader.ReadToEnd();
                }
                LogHelper.Debug(result);
                LogHelper.Error(ex.ToString());
                return JsonConvert.DeserializeObject<T>(result);
                
            }
        }

        public static T OperateRequest<T>(string methodType, string url, Dictionary<string, string> httpParams, bool UsePorxy)
        {
            try
            {
                if (methodType == MethodType.GET.ToString())
                {
                    url = url + "?" + FormatParams(httpParams);
                }
                var req = WebRequest.Create(url) as HttpWebRequest;
                req.Method = methodType;
                req.ContentType = "application/x-www-form-urlencoded";
                
                if (methodType == MethodType.POST.ToString())
                {
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
                //return JsonOperate.JsonToObject<T>(result);
                return JsonConvert.DeserializeObject<T>(result);
            }
            catch (Exception ex)
            {
                LogHelper.Error(ex.ToString());
                return default(T);

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
                LogHelper.Debug(url);
                LogHelper.Debug(result);
                return JsonConvert.DeserializeObject<T>(result);
            }
            catch (WebException ex)
            {
                var result = string.Empty;
                using (var receivedStream = ex.Response.GetResponseStream())
                {
                    var streamReader = new StreamReader(receivedStream);
                    result = streamReader.ReadToEnd();
                }
                LogHelper.Debug(result);
                var obj = JsonConvert.DeserializeObject<T>(result);

                if (!url.ToLower().Contains("mobilecore") && !url.ToLower().Contains("profile"))
                {
                    LogHelper.Error(result + " : " + ex.ToString());
                }


                return obj;

            }
        }



        public static string FormatParams(Dictionary<string, string> httpParams)
        {
            StringBuilder builder = new StringBuilder();
            foreach (KeyValuePair<string, string> kvp in httpParams)
            {
                builder.Append(kvp.Key + "=" +HttpUtility.UrlEncode(TokenCodes.TokenDecode(kvp.Value)) + "&");
                
            }
            return builder.ToString().TrimEnd('&');
        }

        public static T Post<T>(string url, Dictionary<string, string> httpParams)
        {
            return OperateRequest<T>(MethodType.POST.ToString(), url, httpParams);
        }

        public static T Get<T>(string url, Dictionary<string, string> httpParams)
        {
            return OperateRequest<T>(MethodType.GET.ToString(), url, httpParams);
        }

        public static T Post<T>(string url, Dictionary<string, string> httpParams,  bool usePorxy)
        {
            return OperateRequest<T>(MethodType.POST.ToString(), url, httpParams, usePorxy);
        }

        public static T Get<T>(string url, Dictionary<string, string> httpParams,  bool usePorxy)
        {
            return OperateRequest<T>(MethodType.GET.ToString(), url, httpParams, usePorxy);
        }

        public static T Post<T>(string url, Dictionary<string, string> httpParams, Dictionary<string, string> hearderParams = null)
        {
            return OperateRequest<T>(MethodType.POST.ToString(), url, httpParams, hearderParams);
        }

        public static T Get<T>(string url, Dictionary<string, string> httpParams, Dictionary<string, string> hearderParams = null)
        {
            return OperateRequest<T>(MethodType.GET.ToString(), url, httpParams, hearderParams);
        }


    }

    public enum MethodType
    {
        POST,
        GET
    }
}
