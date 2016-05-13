using ServiceStack.Web;

namespace iParty.Services.Interface
{
    public interface IServiceStackQueryCriteria
    {
        void Initialize(IRequest req, IResponse res, object dto);
    }
}
