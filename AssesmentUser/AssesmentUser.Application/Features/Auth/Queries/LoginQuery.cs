using AssesmentUser.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssesmentUser.Application.Features.Auth.Queries
{
    public class LoginQuery : IRequest<string>
    {
        public string? Email { get; set; }
        public string? Password { get; set; }
    }

    public class LoginQueryHandler : IRequestHandler<LoginQuery, string>
    {
        private readonly IJwtProvider _jwtProvider;
        private readonly IApplicationDbContext _dbContext;

        public LoginQueryHandler(IJwtProvider jwtProvider, IApplicationDbContext dbContext)
        {
            _jwtProvider = jwtProvider;
            _dbContext = dbContext;
        }

        public async Task<string> Handle(LoginQuery request, CancellationToken cancellationToken)
        {
            var query = _dbContext.Entity<Domain.User>()
                .FirstOrDefault(x => x.Email!.ToLower().Contains(request.Email!.ToLower()) && !x.IsDeleted);

            if (query is null)
                throw new BadRequestException("email or password is not correct");

            var result = _jwtProvider.Verify(query.Password, request.Password);

            if (!result)
                throw new BadRequestException("email or password is not correct");

            var token = await _jwtProvider.GenerateToken(new UserModel
            {
                IdUser = query.Id.Value,
                UserName = query.UserName,
                Email = query.Email,
                PhoneNumber = query.PhoneNumber,
                Role = query.Role.ToString()
            });

            return token;
        }
    }
}
