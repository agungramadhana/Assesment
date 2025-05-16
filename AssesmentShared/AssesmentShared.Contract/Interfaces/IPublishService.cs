using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssesmentShared.Contract
{
    public interface IPublishService
    {
        Task PublishMessage<T>(T message) where T : class;
    }
}
