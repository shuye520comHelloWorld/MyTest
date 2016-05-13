using ServiceStack;
using ServiceStack.Web;

namespace iParty.Services.Interface.Filters
{
    public class ETagResponseFilterAttribute : ResponseFilterAttribute
    {
        public override void Execute(IRequest req, IResponse res, object requestDto)
        {
            string reqETag, resETag;
            // could be from Version
            var entityHash = req.Items[HttpHeaders.ETag];
            // if the server entity support ETag check
            if (entityHash != null)
            {
                resETag = entityHash.ToString();
                // if client has a IfNoneMatch Header
                if (null != req.Headers[HttpHeaders.IfNoneMatch])
                {
                    reqETag = req.Headers[HttpHeaders.IfModifiedSince] as string;
                    // if entity has been updated
                    if (!string.IsNullOrWhiteSpace(reqETag) && resETag == reqETag)
                    {
                        res.StatusCode = 304;
                    }
                }

                res.AddHeader(HttpHeaders.ETag, resETag);
            }
        }
    }
}
