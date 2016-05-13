using iParty.Services.Interface;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using ServiceStack;

namespace iParty.Services.Infrastructure
{
    public class HttpWebRequestHandler : IHttpRequestHandler
    {
        public T HttpPost<T>(string url, Dictionary<string, string> httpParams)
        {
            return Process<T>("POST", url, httpParams);
        }

        public T HttpGet<T>(string url, Dictionary<string, string> httpParams)
        {
            return Process<T>("GET", url, httpParams);
        }

        public T HttpPost<T>(string url)
        {
            return HttpPost<T>(url, null);
        }

        public T HttpGet<T>(string url)
        {
            return HttpGet<T>(url, null);
        }

        public Task<T> HttpPostAsync<T>(string url, Dictionary<string, string> httpParams)
        {
            return ProcessAsync<T>("POST", url, httpParams);
        }

        public Task<T> HttpGetAsync<T>(string url, Dictionary<string, string> httpParams)
        {
            return ProcessAsync<T>("GET", url, httpParams);
        }

        public Task<T> HttpPostAsync<T>(string url)
        {
            return HttpPostAsync<T>(url, null);
        }

        public Task<T> HttpGetAsync<T>(string url)
        {
            return HttpGetAsync<T>(url, null);
        }

        async Task<T> ProcessAsync<T>(string methodType, string url, Dictionary<string, string> httpParams)
        {
            var result = string.Empty;
            try
            {
                if (methodType == "GET")
                {
                    if (httpParams != null && httpParams.Count > 0)
                    {
                        url = url + "?" + FormatParams(httpParams);
                    }
                }
                var req = HttpWebRequest.Create(url);
                req.Method = methodType;
                string useProxy = string.IsNullOrWhiteSpace(ConfigurationManager.AppSettings["UsePorxy"]) ? "false" : ConfigurationManager.AppSettings["UsePorxy"];
                if (string.Compare(useProxy, "true", true) == 0)
                {
                    WebProxy proxy = new WebProxy(ConfigurationManager.AppSettings["ProxyHost"], int.Parse(ConfigurationManager.AppSettings["ProxyPort"]));
                    req.Proxy = proxy;
                    ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
                }
                if (methodType == "POST")
                {
                    req.ContentType = "application/x-www-form-urlencoded";
                    using (var rs = req.GetRequestStreamAsync())
                    {
                        if (httpParams != null && httpParams.Count > 0)
                        {
                            var formDataString = FormatParams(httpParams);
                            var formData = Encoding.UTF8.GetBytes(formDataString);
                            Stream st = await rs;
                            st.Write(formData, 0, formData.Length);
                        }
                    }
                }

                using (var response = req.GetResponseAsync())
                {
                    var resp = await response;
                    using (var receivedStream = resp.GetResponseStream())
                    {
                        var streamReader = new StreamReader(receivedStream);
                        result = streamReader.ReadToEnd();
                    }
                }

                return result.FromJson<T>();
            }
            catch (WebException ex)
            {
                using (var receivedStream = ex.Response.GetResponseStream())
                {
                    var streamReader = new StreamReader(receivedStream);
                    result = streamReader.ReadToEnd();
                }
                return result.FromJson<T>();
            }
        }

        T Process<T>(string methodType, string url, Dictionary<string, string> httpParams)
        {
            var result = string.Empty;
            try
            {
                if (methodType == "GET")
                {
                    if (httpParams != null && httpParams.Count > 0)
                    {
                        url = url + "?" + FormatParams(httpParams);
                    }
                }
                var req = HttpWebRequest.Create(url);
                req.Method = methodType;
                string useProxy = string.IsNullOrWhiteSpace(ConfigurationManager.AppSettings["UsePorxy"]) ? "false" : ConfigurationManager.AppSettings["UsePorxy"];
                if (string.Compare(useProxy, "true", true) == 0)
                {
                    WebProxy proxy = new WebProxy(ConfigurationManager.AppSettings["ProxyHost"], int.Parse(ConfigurationManager.AppSettings["ProxyPort"]));
                    req.Proxy = proxy;
                    ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
                }
                if (methodType == "POST")
                {
                    req.ContentType = "application/x-www-form-urlencoded";
                    using (var rs = req.GetRequestStream())
                    {
                        if (httpParams != null && httpParams.Count > 0)
                        {
                            var formDataString = FormatParams(httpParams);
                            var formData = Encoding.UTF8.GetBytes(formDataString);
                            rs.Write(formData, 0, formData.Length);
                        }
                    }
                }

                using (var response = req.GetResponse())
                {
                    using (var receivedStream = response.GetResponseStream())
                    {
                        var streamReader = new StreamReader(receivedStream);
                        result = streamReader.ReadToEnd();
                    }
                }

                return result.FromJson<T>();
            }
            catch (WebException ex)
            {
                using (var receivedStream = ex.Response.GetResponseStream())
                {
                    var streamReader = new StreamReader(receivedStream);
                    result = streamReader.ReadToEnd();
                }
                return result.FromJson<T>();
            }
        }

        string FormatParams(Dictionary<string, string> httpParams)
        {
            StringBuilder builder = new StringBuilder();
            foreach (KeyValuePair<string, string> kvp in httpParams)
            {
                builder.Append(kvp.Key + "=" + HttpUtility.UrlEncode(kvp.Value) + "&");
            }
            return builder.ToString().TrimEnd('&');
        }
    }
}
