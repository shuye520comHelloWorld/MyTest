
namespace iParty.Services.Interface
{
    public interface IUserProvider
    {
        IUser GetCurrentUser(long? consultantContactId);
    }
}
