using AssesmentEvent.Application;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace AssesmentEvent.Infrastructure
{
    public class CurrentUserService : ICurrentUserService
    {
        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            IdUser = httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimConstant.IdUser);
            UserName = httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimConstant.UserName);
            Email = httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimConstant.Email);
            PhoneNumber = httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimConstant.PhoneNumber);
            Role = httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimConstant.Role);
        }

        public string? IdUser { get; }

        public string? UserName {  get; }

        public string? Email { get; }

        public string? PhoneNumber { get; }

        public string Role { get; }
    }
}
