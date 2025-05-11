using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssesmentUser.Application.Features.User.Commands
{
    public class UpdateUserQuery : IRequest
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
    }

    public class UpdateUserQueryHandler : IRequestHandler<UpdateUserQuery>
    {
        private readonly IApplicationDbContext _dbContext;

        public UpdateUserQueryHandler(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Unit> Handle(UpdateUserQuery request, CancellationToken cancellationToken)
        {
            var query = await _dbContext.Entity<Domain.User>().FirstOrDefaultAsync(x => x.Id == request.Id);

            if (query is null) throw new NotFoundException("User Not found");

            var emailChecking = _dbContext.Entity<Domain.User>().Any(x => x.Id != request.Id && x.Email!.ToLower() == request.Email.ToLower());

            if (emailChecking) throw new BadRequestException("Email has been registred");

            query.UserName = request.UserName;
            query.Email = request.Email;
            query.PhoneNumber = request.PhoneNumber;

            _dbContext.Entity<Domain.User>().Update(query);

            await _dbContext.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
