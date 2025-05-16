using AssesmentEvent.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssesmentEvent.Application
{
    public class DeleteEventCommand : IRequest
    {
        public Guid Id { get; set; }
    }

    public class DeleteEventCommandHandler : IRequestHandler<DeleteEventCommand>
    {
        private readonly IApplicationDbContext _dbContext;
        
        public DeleteEventCommandHandler(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Unit> Handle(DeleteEventCommand request, CancellationToken cancellationToken)
        {
            var query = await _dbContext.Entity<Event>().FirstOrDefaultAsync(x => x.Id == request.Id && !x.IsDeleted);

            if (query is null) throw new NotFoundException("Event not found");

            query.IsDeleted = true;

            _dbContext.Entity<Event>().Update(query);

            await _dbContext.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
