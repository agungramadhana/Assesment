using AssesmentPayment.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssesmentPayment.Application.Features
{
    public class PaymentModel
    {
        public Guid Id { get; set; }
        public Guid EventCategoryId { get; set; }
        public Guid UserId { get; set; }
        public decimal Amount { get; set; }
        public StatusPaymentEnum PaymentStatus { get; set; }
    }
}
