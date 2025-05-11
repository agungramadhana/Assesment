using AssesmentEvent.Domain.Enums;
using AssesmentEvent.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssesmentEvent.Domain
{
    public class Event : BaseEntity, IEntity
    {
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; } = string.Empty;
        public DateTime? Date { get; set; }
        public string Location { get; set; } = string.Empty;
        
        public virtual ICollection<EventCategories> EventCategories { get; set; } = new HashSet<EventCategories>();
    }
}
