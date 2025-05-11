using AssesmentUser.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssesmentUser.Application.Features
{
    public class GetUserByIdQuery : IRequest<UserModel>
    {
        public Guid Id { get; set; }
    }

    public class GetUserQueryHandler : IRequestHandler<GetUserByIdQuery, UserModel>
    {
        private readonly IApplicationDbContext _dbContext;

        public GetUserQueryHandler(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<UserModel> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            var query = await _dbContext.Entity<Domain.User>().FirstOrDefaultAsync(x => x.Id == request.Id);

            if (query is null)
            {
                throw new NotFoundException("User Not Found");
            }

            return new UserModel
            {
                IdUser = query.Id.GetValueOrDefault(),
                UserName = query.UserName,
                Email = query.Email,
                PhoneNumber = query.PhoneNumber,
                Role = query.Role.ToString()
            };

        }
    }
}
