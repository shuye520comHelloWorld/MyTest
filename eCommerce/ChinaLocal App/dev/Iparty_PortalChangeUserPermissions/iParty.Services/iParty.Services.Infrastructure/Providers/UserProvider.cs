using iParty.Services.Interface;
using iParty.Services.Interface.Exception;
using iParty.Services.ORM.Operations;

namespace iParty.Services.Infrastructure.Providers
{
    public sealed class UserProvider : IUserProvider
    {
        public IUser GetCurrentUser(long? consultantContactId)
        {
            if (consultantContactId.HasValue)
            {
                //TODO: Get consultant level & unit
                var profileOpeation = new ProfileOpeation();
                var result = profileOpeation.ExecEntityProcedure("exec usp_ProfileGet @ContactID", new { ContactID = consultantContactId });
                if (result == null || (result != null && result.Count == 0))
                    throw new NotFoundException(string.Format("the current contactid:{0} was not found", consultantContactId));
                var currentProfile = result[0];
                return new User(consultantContactId.Value, int.Parse(currentProfile.ConsultantLevelID), currentProfile.UnitID, currentProfile.StartDate,currentProfile.BusinessDirector);
            }
            else
            {
                return new User();
            }
        }
    }

}
