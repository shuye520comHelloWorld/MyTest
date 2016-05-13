using System;
using System.Collections.Generic;

namespace iParty.Services.Interface
{
    public interface IUser
    {
        long ContactID();
        bool IsBC();
        bool IsVIP();
        bool IsDirector();
        bool IsNotDirector();
        bool IsPublic();
        DateTime StartDate();
        int Level { get; }
        string Unit { get; }
        List<UserRoles> Roles { get; }
    }

    public enum UserRoles
    {
        Public,
        NoneDirector,
        Director,
        VIP
    }
}
