using AssesmentUser.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssesmentUser.Domain.Seeds
{
    public static class UserSeed
    {
        public static List<User> GetUserSeed()
        {
            var list = new List<User>();

            list.Add(new User
            {
                Id = Guid.Empty,
                UserName = "superadmin",
                Email = "superadmin@mail.com",
                PhoneNumber = "1234567890",
                Role = RoleEnum.Administrator,
            });

            return list;
        }
    }
}
