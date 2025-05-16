using AssesmentEvent.Application;
using AssesmentEvent.Domain;
using AssesmentShared.Contract;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssesmentEvent.Infrastructure
{
    public class PaymentEventConsumer : IConsumer<PaymentEventMessageModel>
    {
        private readonly IApplicationDbContext _dbContext;

        public PaymentEventConsumer(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Consume(ConsumeContext<PaymentEventMessageModel> context)
        {
            var data = context.Message;

            var query = await _dbContext.Entity<EventRegistration>()
                .Where(x => x.EventCategoryId == data.EventCategoryId && x.UserId == data.UserId && x.PaymentStatus == Domain.Enums.EventPaymentEnum.Pending)
                .ToListAsync();

            query.ForEach(x => x.PaymentStatus = Domain.Enums.EventPaymentEnum.Paid);

            _dbContext.Entity<EventRegistration>().UpdateRange(query);

            await _dbContext.SaveChangesAsync();
        }
    }
}
