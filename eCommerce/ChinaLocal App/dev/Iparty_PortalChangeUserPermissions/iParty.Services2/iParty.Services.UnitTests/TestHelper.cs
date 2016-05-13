using Funq;
using iParty.Services.Interface;
using Moq;
using QuartES.Services.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iParty.Services.UnitTests
{
    public static class TestHelper
    {
        private static Container s_container { get; set; }

        public static void Initialize(Container container)
        {
            s_container = container;
        }

        public static void MockUser()
        {
            var user = new Mock<IUser>();
            user.Setup(u => u.ContactID()).Returns(20003605258);
            user.Setup(u => u.IsDirector()).Returns(true);
            user.Setup(u => u.Level).Returns(85);
            user.Setup(u => u.Unit).Returns("001574");
            user.Setup(u => u.Roles).Returns(new List<UserRoles> { UserRoles.Director });

            var context = new Mock<IContext>();
            context.Setup(c => c.ConsultantContactId).Returns(20003605258);
            context.Setup(c => c.UserName).Returns("弓喜英");
            context.Setup(c => c.User).Returns(user.Object);

            s_container.Register<IContext>(container => context.Object);
        }

        public static void MockUser2()
        {
            var user = new Mock<IUser>();
            user.Setup(u => u.ContactID()).Returns(20003492369);
            user.Setup(u => u.IsDirector()).Returns(true);
            user.Setup(u => u.Level).Returns(70);
            user.Setup(u => u.Unit).Returns("002328");
            user.Setup(u => u.Roles).Returns(new List<UserRoles> { UserRoles.Director });

            var context = new Mock<IContext>();
            context.Setup(c => c.ConsultantContactId).Returns(20003492369);
            context.Setup(c => c.UserName).Returns("山也");
            context.Setup(c => c.User).Returns(user.Object);

            s_container.Register<IContext>(container => context.Object);
        }
    }
}
