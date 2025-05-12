using AssesmentShared.Contract;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssesmentEvent.Infrastructure
{
    public class PaymentEventConsumer : IConsumer<PaymentEventMessageModel>
    {
        public Task Consume(ConsumeContext<PaymentEventMessageModel> context)
        {
            throw new NotImplementedException();
        }
    }
}
