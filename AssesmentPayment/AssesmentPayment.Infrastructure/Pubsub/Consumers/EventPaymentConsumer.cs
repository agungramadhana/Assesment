using AssesmentPayment.Application;
using AssesmentPayment.Domain;
using AssesmentShared.Contract;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssesmentPayment.Infrastructure
{
    public class EventPaymentConsumer : IConsumer<PublishEventMessageModel>
    {
        private readonly IApplicationDbContext _dbContext;

        public EventPaymentConsumer(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Consume(ConsumeContext<PublishEventMessageModel> context)
        {
            var data = context.Message;

            _dbContext.Entity<Payment>().Add(new Payment
            {
                EventCategoryId = data.EventCategoryId,
                UserId = data.UserId,
                Amount = data.Amount,
                PaymentStatus = (Domain.EventPaymentEnum)data.PaymentStatus
            });

            await _dbContext.SaveChangesAsync();
        }
    }
}
