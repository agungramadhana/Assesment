using AssesmentEvent.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssesmentEvent.Application.Features
{
    public class CreateEventCommand : CreateEventModel, IRequest
    {
    }

    public class CreateEventCommandHandler : IRequestHandler<CreateEventCommand>
    {
        private readonly IApplicationDbContext _dbContext;

        public CreateEventCommandHandler(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Unit> Handle(CreateEventCommand request, CancellationToken cancellationToken)
        {
            var eventChecking = await _dbContext.Entity<Event>()
                .AnyAsync(x => x.Title.ToLower().Contains(request.Title.ToLower()) && !x.IsDeleted);

            if (eventChecking) throw new BadRequestException("Event already exist");

            var events = new Event
            {
                Title = request.Title,
                Description = request.Description,
                Date = request.Date,
                Location = request.Location
            };

            var eventCategories = new List<EventCategories>();

            foreach (var item in request.Categories)
            {
                eventCategories.Add(new EventCategories
                {
                    EventId = events.Id,
                    Category = item.Category,
                    Price = item.Price,
                    Tiket = item.Tiket
                });
            }

            _dbContext.Entity<Event>().Add(events);
            _dbContext.Entity<EventCategories>().AddRange(eventCategories);

            await _dbContext.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
