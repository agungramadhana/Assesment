using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssesmentEvent.Infrastructure.Authentications
{
    public class UserModel
    {
        public Guid IdUser { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string password { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
    }
}
