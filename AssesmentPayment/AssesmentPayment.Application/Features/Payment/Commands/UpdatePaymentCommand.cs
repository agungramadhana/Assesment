using AssesmentPayment.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AssesmentShared.Contract;

namespace AssesmentPayment.Application.Features
{
    public class UpdatePaymentCommand : IRequest
    {
        [Required]
        public Guid Id { get; set; }
        [Required]
        public Guid EventCategoryId { get; set; }
        [Required]
        public decimal Amount { get; set; }
    }

    public class UpdatePaymentCommandHandler : IRequestHandler<UpdatePaymentCommand>
    {
        private readonly IApplicationDbContext _dbContext;
        private readonly ICurrentUserService _currentUser;
        private readonly IPublishService _publishService;
        public UpdatePaymentCommandHandler(IApplicationDbContext dbContext, ICurrentUserService currentUser, IPublishService publishService)
        {
            _dbContext = dbContext;
            _currentUser = currentUser;
            _publishService = publishService;
        }

        public async Task<Unit> Handle(UpdatePaymentCommand request, CancellationToken cancellationToken)
        {
            var query = await _dbContext.Entity<Payment>().FirstOrDefaultAsync(x => x.Id == request.Id && !x.IsDeleted);

            if (query is null) throw new NotFoundException("Payment not found");

            query.PaymentStatus = Domain.EventPaymentEnum.Paid;

            _dbContext.Entity<Payment>().Update(query);

            await _dbContext.SaveChangesAsync();

            //pubsub to event category
            var message = new PaymentEventMessageModel
            {
                EventCategoryId = request.EventCategoryId,
                UserId = Guid.Parse(_currentUser.IdUser),
                PaymentStatus = AssesmentShared.Contract.EventPaymentEnum.Paid
            };

            await _publishService.PublishMessage(message);

            return Unit.Value;
        }
    }
}
