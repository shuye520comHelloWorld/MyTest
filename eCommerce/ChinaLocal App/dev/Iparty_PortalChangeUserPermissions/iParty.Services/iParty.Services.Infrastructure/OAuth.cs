using iParty.Services.Interface;
using iParty.Services.Interface.Exception;
using NLog;
using QuartES.Services.Common;
using ServiceStack;
using ServiceStack.Caching;
using ServiceStack.Web;
using System;
using System.IO;
using System.Net;
using System.Runtime.Serialization.Json;
using System.Text;

namespace iParty.Services.Infrastructure
{
    public class OAuth : IOAuth
    {
        // http://tools.ietf.org/html/draft-ietf-oauth-v2-bearer-20
        const string BEARER = "Bearer";
        const string MISSING_AUTH = "Bearer realm=\"myCustomers\"";
        const string BAD_TOKEN = "Bearer realm=\"myCustomers\", error=\"invalid_token\", error_description=\"The access token is invalid\"";
        const string EXPIRED_TOKEN = "Bearer realm=\"myCustomers\", error=\"invalid_token\", error_description=\"The access token is expired\"";

        readonly Logger _logger = LogManager.GetCurrentClassLogger();

        public OAuthToken AcquireToken(IRequest req, IResponse res)
        {
            var tokenString = ExtractTokenString(req);
            if (string.IsNullOrEmpty(tokenString))
            {
                _logger.Debug("Ending request with code {0}, message {1}...", 401, MISSING_AUTH);
                res.TerminateWith(401, MISSING_AUTH);
                return null;
            }

            var cache = req.TryResolve<ICacheClient>();
            var cachedOAuthToken = cache.Get<OAuthToken>(tokenString);

            if (cachedOAuthToken == null)
            {
                var result = Validate(tokenString);
                if (string.IsNullOrWhiteSpace(result))
                {
                    throw new OAuthTokenValidationException();
                }
                var oAuthToken = JsonDeserialize<OAuthToken>(result);
                oAuthToken.Token = tokenString;
                _logger.Debug("Got final token object {0}", oAuthToken);

                if (oAuthToken.ResponseCode == "0")
                {
                    _logger.Debug("Validation success, added token object to cache");
                    cache.Add<OAuthToken>(tokenString, oAuthToken, new TimeSpan(0, 10, 0));
                }
                else if (oAuthToken.ResponseCode == "100013")
                {
                    _logger.Debug("Ending request with code {0}, message {1}...", 401, BAD_TOKEN);
                    res.TerminateWith(401, BAD_TOKEN);
                }
                else if (oAuthToken.ResponseCode == "100010")
                {
                    _logger.Debug("Ending request with code {0}, message {1}...", 401, EXPIRED_TOKEN);
                    res.TerminateWith(401, EXPIRED_TOKEN);
                }

                cachedOAuthToken = oAuthToken;
            }
            else
            {
                _logger.Debug("Hit cached token");
                var expireUtcDate = DateTime.Parse(cachedOAuthToken.ExpirationDateUTC);
                if (expireUtcDate <= DateTime.UtcNow.AddSeconds(5))
                {
                    _logger.Debug("Cached Token expired, ending request with code {0}, message {1}...", 401, EXPIRED_TOKEN);
                    cache.Remove(tokenString);
                    res.TerminateWith(401, EXPIRED_TOKEN);
                }
            }

            if (cachedOAuthToken == null)
            {
                throw new OAuthTokenValidationException();
            }

            return cachedOAuthToken;
        }

        string Validate(string tokenString)
        {
            var url = ConfigHelper.GetLocalizedAppSetting("OAuthServiceURL");
            _logger.Debug("Validating access token string {0} against {1}", tokenString, url);
            try
            {
                var req = WebRequest.Create(url) as HttpWebRequest;
                req.Method = "POST";
                req.ContentType = "application/x-www-form-urlencoded";
                using (var rs = req.GetRequestStream())
                {
                    var formDataString = "access_token=" + tokenString;
                    var formData = Encoding.UTF8.GetBytes(formDataString);
                    rs.Write(formData, 0, formData.Length);
                }

                var res = req.GetResponse();
                var result = string.Empty;
                using (var receivedStream = res.GetResponseStream())
                {
                    var streamReader = new StreamReader(receivedStream);
                    result = streamReader.ReadToEnd();
                }
                _logger.Debug("Validating access token string {0} against {1} resulting {2}", tokenString, url, result);

                return result;
            }
            catch (WebException ex)
            {
                _logger.Error("Validation on access token string {0} against {1}, failed because {2}", tokenString, url, ex.Message);
                throw new OAuthTokenValidationException(ex.Message, ex.InnerException);
            }
        }

        string ExtractTokenString(IRequest req)
        {
            var tokenString = string.Empty;
            var authz = req.Headers[HttpHeaders.Authorization];
            if (authz != null && authz.Contains(BEARER))
            {
                _logger.Debug("Reading access token from Header base on Bearer scheme...");
                tokenString = authz.TrimStart(BEARER.ToCharArray()).Trim();
            }
            else
            {
                _logger.Debug("Reading access token from request parameter...");
                tokenString = req.GetParam("access_token");
            }
            return tokenString;
        }

        T JsonDeserialize<T>(string txt)
        {
            if (string.IsNullOrWhiteSpace(txt))
            {
                return default(T);
            }

            var ser = new DataContractJsonSerializer(typeof(T));
            using (var ms = new MemoryStream(Encoding.UTF8.GetBytes(txt)))
            {
                return (T)ser.ReadObject(ms);
            }
        }

        public bool HasTokenString(IRequest req)
        {
            var tokenString = ExtractTokenString(req);
            return !string.IsNullOrWhiteSpace(tokenString);
        }
    }
}
