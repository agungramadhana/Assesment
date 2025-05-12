using AssesmentEvent.Domain;
using AssesmentEvent.Domain.Enums;
using AssesmentShared.Contract;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssesmentEvent.Application.Features
{
    public class CreateOrderEventCommand : IRequest
    {
        [Required]
        public Guid Id { get; set; }
        [Required]
        public Guid EventId { get; set; }
        [Required]
        public int TotalTiket { get; set; }
    }

    public class CreateOrderEventCommandHandler : IRequestHandler<CreateOrderEventCommand>
    {
        private readonly IApplicationDbContext _dbContext;
        private readonly ICurrentUserService _currentUser;
        private readonly IPublishService _publishService;

        public CreateOrderEventCommandHandler(IApplicationDbContext dbContext, ICurrentUserService currentUser, IPublishService publishService)
        {
            _dbContext = dbContext;
            _currentUser = currentUser;
            _publishService = publishService;
        }

        public async Task<Unit> Handle(CreateOrderEventCommand request, CancellationToken cancellationToken)
        {
            var events = await _dbContext.Entity<Event>()
                .Include(x => x.EventCategories)
                    .ThenInclude(x => x.EventRegistrations)
                .FirstOrDefaultAsync(x => x.Id == request.EventId, cancellationToken);

            if (events is null)
                throw new NotFoundException("Event not found");

            var eventCategory = events.EventCategories
                .FirstOrDefault(x => x.Id == request.Id);

            if (eventCategory is null)
                throw new NotFoundException("Event category not found");

            var reservedCount = eventCategory.EventRegistrations
                .Count(y => y.PaymentStatus == Domain.Enums.EventPaymentEnum.Pending || y.PaymentStatus == Domain.Enums.EventPaymentEnum.Paid);

            var availableTiket = eventCategory.Tiket - reservedCount;

            if (availableTiket < request.TotalTiket)
                throw new BadRequestException("Tiket unavailable");

            for (int i = 0; i < request.TotalTiket; i++)
            {
                _dbContext.Entity<EventRegistration>().Add(new EventRegistration
                {
                    EventCategoryId = request.Id,
                    UserId = Guid.Parse(_currentUser.IdUser),
                    PaymentStatus = Domain.Enums.EventPaymentEnum.Pending
                });
            }

            await _dbContext.SaveChangesAsync();

            //pubsub to payment
            var data = new PublishEventMessageModel
            {
                EventCategoryId = request.Id,
                UserId = Guid.Parse(_currentUser.IdUser),
                Amount = request.TotalTiket * eventCategory.Price,
                PaymentStatus = AssesmentShared.Contract.EventPaymentEnum.Pending,
            };

            await _publishService.PublishMessage(data);

            return Unit.Value;
        }
    }

}
