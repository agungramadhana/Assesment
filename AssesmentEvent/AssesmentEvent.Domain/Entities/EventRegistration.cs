using AssesmentEvent.Domain.Enums;
using AssesmentEvent.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssesmentEvent.Domain
{
    public class EventRegistration : BaseEntity, IEntity
    {
        public Guid EventCategoryId { get; set; }
        public Guid UserId { get; set; }
        public EventPaymentEnum PaymentStatus { get; set; }

        [ForeignKey(nameof(EventCategoryId))]
        public virtual EventCategories EventCategories { get; set; }
    }
}
