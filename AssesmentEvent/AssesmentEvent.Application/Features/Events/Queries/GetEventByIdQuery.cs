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
    public class GetEventByIdQuery : IRequest<EventModel>
    {
        public Guid Id { get; set; }
    }

    public class GetEventByIdQueryHandle : IRequestHandler<GetEventByIdQuery, EventModel>
    {
        private readonly IApplicationDbContext _dbContext;

        public GetEventByIdQueryHandle(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<EventModel> Handle(GetEventByIdQuery request, CancellationToken cancellationToken)
        {
            var query = await _dbContext.Entity<Event>()
                .Include(x => x.EventCategories)
                    .ThenInclude(x => x.EventRegistrations)
                .FirstOrDefaultAsync(x => x.Id == request.Id && !x.IsDeleted);

            if (query is null) throw new NotFoundException("Event not found");

            return new DetailEventModel
            {
                Id = query.Id,
                Title = query.Title,
                Description = query.Description,
                Date = query.Date.GetValueOrDefault(),
                Location = query.Location,
                Categories = query.EventCategories.Select(x => new EventCategoryModel
                {
                    Id = x.Id,
                    EventId = x.EventId,
                    Category = x.Category,
                    CategoryName = x.Category.ToString(),
                    Price = x.Price,
                    Tiket = x.Tiket - x.EventRegistrations.Count(y => y.PaymentStatus == EventPaymentEnum.Pending || y.PaymentStatus == EventPaymentEnum.Paid)
                }).OrderByDescending(x => x.Category).ToList()
            };
        }
    }
}
