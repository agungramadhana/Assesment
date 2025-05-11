using AssesmentUser.Application.Features;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssesmentUser.Application
{
    public interface IJwtProvider
    {
        Task<string> GenerateToken(UserModel user);
        string Hash(string password);
        bool Verify(string userPassword, string password);
    }
}
