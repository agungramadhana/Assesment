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
    public class UpdateEventCategoryCommand : List<UpdateEventCategoryModel>, IRequest
    {
    }

    public class UpdateEventCategoryCommandHandler : IRequestHandler<UpdateEventCategoryCommand>
    {
        private readonly IApplicationDbContext _dbContext;

        public UpdateEventCategoryCommandHandler(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Unit> Handle(UpdateEventCategoryCommand request, CancellationToken cancellationToken)
        {
            var eventCategories = await _dbContext.Entity<EventCategories>()
                .Where(x => request.Select(a => a.EventId).Contains(x.EventId) && !x.IsDeleted)
                .ToListAsync();

            foreach (var item in request)
            {
                if (item.Id == Guid.Empty || string.IsNullOrEmpty(item.Id.ToString()))
                {
                    _dbContext.Entity<EventCategories>().Add(new EventCategories
                    {
                        EventId = item.EventId,
                        Category = item.Category,
                        Price = item.Price,
                        Tiket = item.Tiket
                    });
                }
                else
                {
                    var category = eventCategories.FirstOrDefault(x => x.Id == item.Id);
                    if (category == null) throw new NotFoundException("Category not found");

                    var categoryChecking = eventCategories.FirstOrDefault(x => x.Id != item.Id && x.Category == item.Category);
                    if (categoryChecking != null) throw new BadRequestException("Category already exist");

                    category.Category = item.Category;
                    category.Price = item.Price;
                    category.Tiket = item.Tiket;

                    _dbContext.Entity<EventCategories>().Update(category);
                }
            }

            await _dbContext.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
