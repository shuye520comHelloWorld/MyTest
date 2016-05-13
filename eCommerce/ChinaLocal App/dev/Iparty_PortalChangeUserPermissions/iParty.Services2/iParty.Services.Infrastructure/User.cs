using iParty.Services.Interface;
using System;
using System.Collections.Generic;

namespace iParty.Services.Infrastructure
{
    public sealed class User : IUser
    {
        public User(long? consultantContactId = null, int? level = null, string unit = null, DateTime? startDate = null, string BusinessDirector=null)
        {
            Roles = new List<UserRoles>();
            if (consultantContactId.HasValue && level.HasValue && !string.IsNullOrEmpty(unit))
            {
                ContactID = consultantContactId.Value;
                Level = level.Value;
                Unit = unit;
                StartDate = startDate.Value;
                if (level.Value >= 60 && BusinessDirector == "Y")
                {
                    Roles.Add(UserRoles.Director);
                }
                else
                {
                    Roles.Add(UserRoles.NoneDirector);

                    if (level.Value < 15)
                    {
                        Roles.Add(UserRoles.VIP);
                    }
                }
            }
            else
            {
                Roles.Add(UserRoles.Public);
            }
        }

        private User() { }
        public long ContactID { get; private set; }
        public List<UserRoles> Roles { get; private set; }
        public int Level { get; private set; }
        public string Unit { get; private set; }

        public DateTime StartDate { get; private set; }
        public bool IsPublic()
        {
            return this.Roles.Contains(UserRoles.Public);
        }

        public bool IsBC()
        {
            return this.Roles.Contains(UserRoles.Director) || this.Roles.Contains(UserRoles.NoneDirector);
        }

        public bool IsVIP()
        {
            return this.Roles.Contains(UserRoles.VIP);
        }

        public bool IsNotDirector()
        {
            return this.Roles.Contains(UserRoles.NoneDirector);
        }

        public bool IsDirector()
        {
            return this.Roles.Contains(UserRoles.Director);
        }

        long IUser.ContactID()
        {
            return this.ContactID;
        }
        DateTime IUser.StartDate()
        {
            return this.StartDate;
        }
    }
}
