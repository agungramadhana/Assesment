using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace AssesmentUser.Application.Features
{
    public class GetListUserQuery : IRequest<List<UserModel>>
    {
    }

    public class GetListUserQueryHandler : IRequestHandler<GetListUserQuery, List<UserModel>>
    {
        private readonly IApplicationDbContext _dbContext;
        public GetListUserQueryHandler(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<UserModel>> Handle(GetListUserQuery request, CancellationToken cancellationToken)
        {
            var query = await _dbContext.Entity<Domain.User>()
                .Where(x => !x.IsDeleted)
                .OrderByDescending(x => x.CreatedAt)
                .Select(x => new UserModel
                {
                    IdUser = x.Id.GetValueOrDefault(),
                    UserName = x.UserName,
                    Email = x.Email,
                    PhoneNumber = x.PhoneNumber,
                    Role = x.Role.ToString()
                }).ToListAsync();

            return query;
        }
    }
}
