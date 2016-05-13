using ServiceStack.Web;

namespace iParty.Services.Interface
{
    public interface IOAuth
    {
        OAuthToken AcquireToken(IRequest req, IResponse res);
        bool HasTokenString(IRequest req);
    }
}
