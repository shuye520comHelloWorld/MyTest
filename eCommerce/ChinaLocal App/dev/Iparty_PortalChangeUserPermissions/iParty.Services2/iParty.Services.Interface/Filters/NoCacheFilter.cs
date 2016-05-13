using ServiceStack;
using ServiceStack.Web;

namespace iParty.Services.Interface.Filters
{
    public class NoCacheResponseFilterAttribute : ResponseFilterAttribute
    {
        public override void Execute(IRequest req, IResponse res, object requestDto)
        {
            res.AddHeader(HttpHeaders.CacheControl, "max-age=0");
        }
    }
}
