using ServiceStack;
using ServiceStack.Web;
using System;

namespace iParty.Services.Interface.Filters
{
    public class LastModifiedResponseFilterAttribute : ResponseFilterAttribute
    {
        public override void Execute(IRequest req, IResponse res, object requestDto)
        {
            DateTime reqLastModified, resLastModified;
            var entityLastModified = req.Items[HttpHeaders.LastModified];
            // if the server entity support LastModified check
            if (entityLastModified != null)
            {
                resLastModified = (DateTime)entityLastModified;
                // if client has a IfModifiedSince Header
                if (null != req.Headers[HttpHeaders.IfModifiedSince])
                {
                    DateTime.TryParse(req.Headers[HttpHeaders.IfModifiedSince], out reqLastModified);
                    // if entity has been updated
                    if (reqLastModified != null && reqLastModified != DateTime.MinValue && resLastModified > reqLastModified)
                    {
                        res.StatusCode = 304;
                    }
                }

                res.AddHeader(HttpHeaders.LastModified, resLastModified.ToString());
            }
        }
    }
}
