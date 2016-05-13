using ServiceStack;
using System.Reflection;
using ServiceStack.Validation;
using IParty.Services.Internal.Validator;
using IParty.Services.Internal.Infrastructure;
using IParty.Services.OAuth;
using ServiceStack.Caching;
using IParty.Services.Common.Exception;
using IParty.Services.Common;
using Funq;
using IParty.Services.Internal.Interface;

namespace IParty.Services.Internal.Host
{
    public class IPartyInternalHost : AppHostBase
    {
        public IPartyInternalHost()
            : base("IParty internal Service", typeof(IParty.Services.Internal.API.EventServices).Assembly) { }

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
                    { typeof(NotFoundWithNoEmailException), 404 },
                    { typeof(CommandException), 400 },
                    { typeof(PermessionException), 403 },
                    { typeof(RequestErrorException), 400 },
                    { typeof(OAuthTokenValidationException), 500 },
                }
            });

            // Validation settings
            Plugins.Add(new ValidationFeature());
            container.RegisterValidators(typeof(CommandRequestDtoValidator<>).Assembly);

            //Register basic IOC container
            container.Register<IOAuth>(new IParty.Services.OAuth.OAuth());
            container.Register<ICacheClient>(new MemoryCacheClient());

            // The API is protected by OAuth, so just enable CORS request from any(*) domain
            Plugins.Add(new CorsFeature());
            RegisterRepositories(container);
            //Register Filter
            base.GlobalRequestFilters.Add(IParty.Services.OAuth.Filters.GlobalRequestFilters.ValidateCommandId);
            base.GlobalRequestFilters.Add(IParty.Services.OAuth.Filters.GlobalRequestFilters.ValidateContext);

            //add the error info to the log file
            this.ServiceExceptionHandlers.Add((httpReq, request, exception) =>
            {
                if (!(exception is NotFoundWithNoEmailException))
                {
                    BaseRequestDto dto = request.GetDto() as BaseRequestDto;
                    var ErrorMessage = string.Empty;
                    var InnerExceptionMessage = string.Empty;
                    if (exception.InnerException != null)
                        InnerExceptionMessage = exception.InnerException.Message;
                    ErrorMessage = string.Format("\r\nMessage:{0}\r\n,StackTrace:{1}\r\n,InnerException:{2}",
                            exception.Message, exception.StackTrace, InnerExceptionMessage);
                    LogHelper.Error(ErrorMessage);
                }
                return DtoUtils.CreateErrorResponse(request, exception);
            });

            // The following is to allow CORS 'preflight' request w/ an OPTIONS request
            base.PreRequestFilters.Add(
                (req, res) =>
                {
                    if (req.Verb == HttpMethods.Options)
                    {
                        res.EndRequest();
                    }
                });
        }

        private void RegisterRepositories(Container container)
        {
            container.RegisterAutoWiredAs<EventRepository, IEventRepository>();
        }
    }
}