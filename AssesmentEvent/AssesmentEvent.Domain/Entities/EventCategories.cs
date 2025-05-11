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
    public class EventCategories : BaseEntity, IEntity
    {
        public Guid EventId { get; set; }
        public EventCategoryEnum Category { get; set; }
        public decimal Price { get; set; }
        public int Tiket { get; set; }

        [ForeignKey(nameof(EventId))]
        public virtual Event Event { get; set; }

        public virtual ICollection<EventRegistration> EventRegistrations { get; set; } = new HashSet<EventRegistration>();
    }
}
