using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AssesmentShared.Contract;
using MassTransit;

namespace AssesmentPayment.Infrastructure
{
    public class PublishService : IPublishService
    {
        private readonly IBusControl _bus;

        public PublishService(IBusControl bus)
        {
            _bus = bus;
        }

        public async Task PublishMessage<T>(T message) where T : class
        {
            await _bus.Publish(message);
        }
    }
}
