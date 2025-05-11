using AssesmentEvent.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssesmentEvent.Application.Features.Events.Models
{
    public class EventModel
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; } = string.Empty;
        public DateTime Date { get; set; }
        public string Location { get; set; } = string.Empty;
    }

    public class DetailEventModel : EventModel
    {
        public List<EventCategoryModel> Categories { get; set; } = new List<EventCategoryModel>();
    }

    public class EventCategoryModel
    {
        public Guid Id { get; set; }
        public Guid EventId { get; set; }
        public EventCategoryEnum Category { get; set; }
        public string CategoryName { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int Tiket { get; set; }
    }
}
