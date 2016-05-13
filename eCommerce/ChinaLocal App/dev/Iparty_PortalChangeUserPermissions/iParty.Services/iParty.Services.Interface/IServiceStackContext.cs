using QuartES.Services.Core;
using ServiceStack.Web;

namespace iParty.Services.Interface
{
    public interface IServiceStackContext : IContext
    {
        void Initialize(IRequest req, IResponse res, object dto);
    }
}
