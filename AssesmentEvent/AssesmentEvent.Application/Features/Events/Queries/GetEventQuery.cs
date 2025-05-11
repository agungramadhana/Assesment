using AssesmentEvent.Application.Features.Events.Models;
using AssesmentEvent.Domain;
using AssesmentEvent.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssesmentEvent.Application.Features
{
    public class GetEventQuery : IRequest<List<EventModel>>
    {
    }

    public class GetEventQueryHandler : IRequestHandler<GetEventQuery, List<EventModel>>
    {
        private readonly IApplicationDbContext _dbContext;

        public GetEventQueryHandler(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<EventModel>> Handle(GetEventQuery request, CancellationToken cancellationToken)
        {
            var query = await _dbContext.Entity<Event>()
                .Include(x => x.EventCategories)
                    .ThenInclude(x => x.EventRegistrations)
                .OrderByDescending(x => x.CreatedAt)
                .Select(x => new EventModel
                {
                    Id = x.Id,
                    Title = x.Title,
                    Description = x.Description,
                    Date = x.Date.GetValueOrDefault(),
                    Location = x.Location,
                }).ToListAsync();

            return query;

        }
    }
}
