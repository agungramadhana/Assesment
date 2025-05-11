using AssesmentUser.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssesmentUser.Infrastructure
{
    public static class ClaimConstant
    {
        public const string IdUser = "idUser";
        public const string UserName = "userName";
        public const string Email = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress";
        public const string PhoneNumber = "phonenumber";
        public const string Role = "role";
    }
}
