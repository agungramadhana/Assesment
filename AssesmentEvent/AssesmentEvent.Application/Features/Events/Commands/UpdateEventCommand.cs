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
    public class UpdateEventCommand : UpdateEventModel, IRequest
    {
    }

    public class UpdateEventCommandHandler : IRequestHandler<UpdateEventCommand>
    {
        private readonly IApplicationDbContext _dbContext;

        public UpdateEventCommandHandler(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Unit> Handle(UpdateEventCommand request, CancellationToken cancellationToken)
        {
            var query = await _dbContext.Entity<Event>().FirstOrDefaultAsync(x => x.Id == request.Id && !x.IsDeleted);

            if (query is null) throw new NotFoundException("Event not found");

            query.Title = request.Title;
            query.Description = request.Description;
            query.Location = request.Location;
            query.Date = request.Date;

            _dbContext.Entity<Event>().Update(query);

            await _dbContext.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
