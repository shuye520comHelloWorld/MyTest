using iParty.Services.Contract;
using NLog;
using QuartES.Services.Core;
using ServiceStack.Web;
using System;

namespace iParty.Services.Interface.Filters
{
    public static class GlobalRequestFilters
    {
        static readonly Logger _logger = LogManager.GetCurrentClassLogger();

        public static void LogRequest(IRequest req, IResponse res, object dto)
        {
        }

        public static void InitContext(IRequest req, IResponse res, object dto)
        {
            _logger.Debug("Resolving request Context...");
            var context = req.TryResolve<IContext>() as IServiceStackContext;
            context.Initialize(req, res, dto);
        }

        public static void InitQueryCriteria(IRequest req, IResponse res, object dto)
        {
            _logger.Debug("Resolving request QueryCriteria...", dto);
            var queryCriteria = req.TryResolve<IQueryCriteria>() as IServiceStackQueryCriteria;
            queryCriteria.Initialize(req, res, dto);
        }

        public static void ValidateContext(IRequest req, IResponse res, object dto)
        {
            var context = req.TryResolve<IContext>();
            _logger.Debug("Validating Context...");
           /* if (string.IsNullOrWhiteSpace(context.SubsidiaryCode))
            {
                _logger.Warn("Missing mandatory SubsidiaryCode on operation {0}'s Context", dto);
                throw new ArgumentNullException("Context.SubsidiaryCode");
            }
            if (string.IsNullOrWhiteSpace(context.ClientKey))
            {
                _logger.Warn("Missing mandatory ClientKey on operation {0}'s Context", dto);
                throw new ArgumentNullException("Context.ClientKey");
            }
            * */
            if (context.User.IsBC() && context.ConsultantContactId <= 0)
            {
                _logger.Warn("Missing mandatory ConsultantContactId on operation {0}'s Context", dto);
                throw new ArgumentNullException("Context.ConsultantContactId");
            }
            if (string.IsNullOrWhiteSpace(context.ClientIdentity))
            {
                _logger.Warn("Missing mandatory ClientIdentity on operation {0}'s Context", dto);
                throw new ArgumentNullException("Context.ClientIdentity");
            }
            _logger.Debug("Context validated.");
        }
    }
}
