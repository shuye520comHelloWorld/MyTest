using Funq;
using iParty.Services.Infrastructure;
using iParty.Services.Infrastructure.Providers;
using iParty.Services.Interface;
using iParty.Services.Interface.Exception;
using iParty.Services.Validator;
using NLog;
using QuartES.Services.Core;
using ServiceStack;
using ServiceStack.Caching;
using ServiceStack.Validation;
using System.Net;
using myFilters = iParty.Services.Interface.Filters;

namespace iParty.Services.Host
{
    public class ASPNetHost : AppHostBase
    {
        public ASPNetHost()
            : base("iParty Service", typeof(iParty.Services.Impl.PartyApplicationService).Assembly) { }

        public override void Configure(Container container)
        {
            // Host settings
            ServiceStack.Text.JsConfig.EmitCamelCaseNames = false;
            Feature disableFeatures = Feature.Jsv | Feature.Soap | Feature.Csv | Feature.Xml;
            SetConfig(new HostConfig
            {
                DefaultContentType = "application/json",
                EnableFeatures = Feature.All.Remove(disableFeatures),
#if DEBUG
                DebugMode = true,
#endif
                MapExceptionToStatusCode = {
                    { typeof(NotFoundException), 404 },
                    { typeof(OAuthTokenValidationException), 500 },
                    {typeof(InvalidRequestException), 400}
                }

            });

            this.ServiceExceptionHandlers.Add((httpReq, request, exception) =>
            {
                //log your exceptions here,only log the 500 error 
                if (exception.GetStatus() == HttpStatusCode.InternalServerError)
                {
                    LogHelper.Error(exception.Message + "\r\n" + exception.StackTrace);
                }

                //call default exception handler or prepare your own custom response
                return DtoUtils.CreateErrorResponse(request, exception);
            });
            this.UncaughtExceptionHandlers.Add((req, res, operationName, ex) =>
            {
                if (ex is NotFoundException)res.StatusCode = 404;
                if (ex is OAuthTokenValidationException) res.StatusCode = 500;
                if (ex is InvalidRequestException) res.StatusCode = 400;
                if (ex.GetStatus() == HttpStatusCode.InternalServerError)
                {
                    LogHelper.Error(ex.Message + "\r\n" + ex.StackTrace);
                }
                res.Write("Error: {0}: {1}".Fmt(ex.GetType().Name, ex.Message));
                res.EndRequest(skipHeaders: true);
            });
            // Validation settings
            Plugins.Add(new ValidationFeature());
            container.RegisterValidators(typeof(QueryPartyValidator).Assembly);

            // Global IoC settings
            // container.Register<IOAuth>(new OAuth());
            container.Register<ICacheClient>(new MemoryCacheClient());
            container.RegisterAutoWiredAs<ThreeDES, IThreeDES>();
            container.RegisterAutoWiredAs<UserProvider, IUserProvider>();
            container.RegisterAutoWiredAs<HttpWebRequestHandler, IHttpRequestHandler>();

            // Per HttpRequest IoC settings
            container.RegisterAutoWiredAs<OAuth, IOAuth>().ReusedWithin(ReuseScope.Request);
            container.RegisterAutoWiredAs<Context, IContext>().ReusedWithin(ReuseScope.Request);
            container.RegisterAutoWiredAs<HttpQueryCriteria, IQueryCriteria>().ReusedWithin(ReuseScope.Request);
            container.RegisterAutoWiredAs<PartyQuery, IPartyQuery>().ReusedWithin(ReuseScope.Request);
            container.RegisterAutoWiredAs<EventsProvider, IEventsProvider>().ReusedWithin(ReuseScope.Request);
            container.RegisterAutoWiredAs<WorkshopsProvider, IWorkshopsProvider>().ReusedWithin(ReuseScope.Request);
            container.RegisterAutoWiredAs<PartyRepository, IPartyRepository>().ReusedWithin(ReuseScope.Request);
            container.RegisterAutoWiredAs<PartyApplicationRepository, IPartyApplicationRepository>().ReusedWithin(ReuseScope.Request);
            container.RegisterAutoWiredAs<InvitationRepository, IInvitationRepository>().ReusedWithin(ReuseScope.Request);
            container.RegisterAutoWiredAs<WeChartCardRepository, IWeChartCardRepository>().ReusedWithin(ReuseScope.Request);

            // The API is protected by OAuth, so just enable CORS request from any(*) domain
            Plugins.Add(new CorsFeature());
            // The following is to allow CORS 'preflight' request w/ an OPTIONS request
            base.PreRequestFilters.Add(
                (req, res) =>
                {
                    if (req.Verb == HttpMethods.Options)
                    {
                        res.EndRequest();
                    }
                });

            // Global Reqeust Filters
            base.GlobalRequestFilters.Add(myFilters.GlobalRequestFilters.InitContext);
            base.GlobalRequestFilters.Add(myFilters.GlobalRequestFilters.InitQueryCriteria);
            base.GlobalRequestFilters.Add(myFilters.GlobalRequestFilters.ValidateContext);

        }
    }

    public class LogHelper
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        public static void Info(string message)
        {
            Logger.Debug(message);
        }

        public static void Warn(string message)
        {
            Logger.Warn(message);
        }

        public static void Error(string message)
        {
            Logger.Error(message);
        }

        public static void Fatal(string message)
        {
            Logger.Fatal(message);
        }
    }
}