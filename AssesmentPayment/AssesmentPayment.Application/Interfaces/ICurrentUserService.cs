using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssesmentPayment.Application
{
    public interface ICurrentUserService
    {
        string IdUser { get; }
        string UserName { get; }
        string Email { get; }
        string PhoneNumber { get; }
        string Role { get; }
    }
}
