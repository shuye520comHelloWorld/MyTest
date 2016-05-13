using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace PinkBus.Services.Common
{
    public static class ServiceHttpRequest
    {
        public static T OperateRequest<T>(string methodType, string url, Dictionary<string, string> httpParams)
        {
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
            return JsonOperate.JsonToObject<T>(result);
        }

        public static string FormatParams(Dictionary<string, string> httpParams)
        {
            StringBuilder builder = new StringBuilder();
            foreach (KeyValuePair<string, string> kvp in httpParams)
            {
                builder.Append(kvp.Key + "=" + kvp.Value + "&");
            }
            return builder.ToString();
        }

        public static T Post<T>(string url, Dictionary<string, string> httpParams)
        {
            return OperateRequest<T>(MethodType.POST.ToString(), url, httpParams);
        }

        public static T Get<T>(string url, Dictionary<string, string> httpParams)
        {
            return OperateRequest<T>(MethodType.GET.ToString(), url, httpParams);
        }
    }

    public enum MethodType
    {
        POST,
        GET
    }
}
