using AssesmentUser.Domain;
using AssesmentUser.Domain.Seeds;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssesmentUser.Application.Seeds
{
    public class UserSeedCommand : IRequest
    {

    }

    public class UserSeedCommandHandler : IRequestHandler<UserSeedCommand>
    {
        private readonly IApplicationDbContext _dbContext;
        private readonly IJwtProvider _jwtProvide;
        private const string passwordAdmin = "PasswordAdmin";
        public UserSeedCommandHandler(IApplicationDbContext dbContext, IJwtProvider jwtProvider)
        {
            _dbContext = dbContext;
            _jwtProvide = jwtProvider;
        }

        public async Task<Unit> Handle(UserSeedCommand request, CancellationToken cancellationToken)
        {
            var userSeed = UserSeed.GetUserSeed();

            var user = await _dbContext.Entity<User>().Where(x => !x.IsDeleted).ToListAsync(cancellationToken);

            foreach (var item in userSeed)
            {
                if (!user.Any(x => x.Id == item.Id))
                {
                    _dbContext.Entity<User>().Add(new User
                    { 
                        Id = item.Id,
                        UserName = item.UserName,
                        Email = item.Email,
                        PhoneNumber = item.PhoneNumber,
                        Password = _jwtProvide.Hash(passwordAdmin),
                        Role = item.Role
                    });
                }
            }

            await _dbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
