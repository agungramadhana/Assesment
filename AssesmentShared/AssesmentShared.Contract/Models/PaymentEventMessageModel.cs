using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssesmentShared.Contract
{
    public class PaymentEventMessageModel
    {
        public Guid EventCategoryId { get; set; }
        public Guid UserId { get; set; }
        public EventPaymentEnum PaymentStatus { get; set; }
    }
}
