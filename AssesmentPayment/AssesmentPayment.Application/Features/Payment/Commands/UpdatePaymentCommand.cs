using AssesmentPayment.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public UpdatePaymentCommandHandler(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Unit> Handle(UpdatePaymentCommand request, CancellationToken cancellationToken)
        {
            var query = await _dbContext.Entity<Payment>().FirstOrDefaultAsync(x => x.Id == request.Id);

            if (query is null) throw new NotFoundException("Payment not found");

            query.PaymentStatus = EventPaymentEnum.Paid;

            _dbContext.Entity<Payment>().Update(query);

            await _dbContext.SaveChangesAsync();

            //pubsub to event category

            return Unit.Value;
        }
    }
}
