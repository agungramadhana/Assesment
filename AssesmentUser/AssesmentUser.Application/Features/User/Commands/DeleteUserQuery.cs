using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssesmentUser.Application.Features
{
    public class DeleteUserQuery : IRequest
    {
        public Guid Id { get; set; }
    }

    public class DeleteUserQueryHandler : IRequestHandler<DeleteUserQuery>
    {
        private readonly IApplicationDbContext _dbContext;

        public DeleteUserQueryHandler(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<Unit> Handle(DeleteUserQuery request, CancellationToken cancellationToken)
        {
            var query = await _dbContext.Entity<Domain.User>().FirstOrDefaultAsync(x => x.Id == request.Id);

            if (query is null) throw new NotFoundException("User Not found");

            query.IsDeleted = true;

            _dbContext.Entity<Domain.User>().Update(query);

            await _dbContext.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
