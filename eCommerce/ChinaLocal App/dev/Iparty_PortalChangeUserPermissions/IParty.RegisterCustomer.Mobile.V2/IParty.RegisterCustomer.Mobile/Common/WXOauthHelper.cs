using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Newtonsoft.Json;
using System.IO;
using System.Net;
using System.Net.Http;

namespace IParty.RegisterCustomer.Mobile.Common
{
    public class WXOauthHelper
    {

       /// <summary>
        /// get content from the specified url
        /// </summary>
        /// <param name="url">url address</param>
        /// <returns></returns>
        public static string GetContent(string url)
        {
            ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
            var client =
                   new HttpClient() { MaxResponseContentBufferSize = 100000000 };
            var content = client.GetStringAsync(url);
            return content.Result;
        }
        public static TResponse PostData<T, TResponse>(string url, T parms)
        {
            ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
            var client =
                   new HttpClient() { MaxResponseContentBufferSize = 100000000 };
            HttpResponseMessage req = client.PostAsJsonAsync<T>(url, parms).Result;
            var sRe = req.Content.ReadAsStringAsync().Result;
            return req.Content.ReadAsAsync<TResponse>().Result;

        }
    }
}