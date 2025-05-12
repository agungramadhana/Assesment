using AssesmentPayment.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssesmentPayment.Domain
{
    public class Payment : BaseEntity, IEntity
    {
        public Guid EventCategoryId { get; set; }
        public Guid UserId { get; set; }
        public decimal Amount { get; set; }
        public EventPaymentEnum PaymentStatus { get; set; } = EventPaymentEnum.Pending;
    }
}
