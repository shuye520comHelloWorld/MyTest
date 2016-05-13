using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iParty.Services.Interface
{
    public interface IHttpRequestHandler
    {
        T HttpPost<T>(string url, Dictionary<string, string> httpParams);
        T HttpGet<T>(string url, Dictionary<string, string> httpParams);
        T HttpPost<T>(string url);
        T HttpGet<T>(string url);
        Task<T> HttpPostAsync<T>(string url, Dictionary<string, string> httpParams);
        Task<T> HttpGetAsync<T>(string url, Dictionary<string, string> httpParams);
        Task<T> HttpPostAsync<T>(string url);
        Task<T> HttpGetAsync<T>(string url);
    }
}
