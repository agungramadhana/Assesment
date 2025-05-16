using AssesmentEvent.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssesmentEvent.Application
{
    public class UpdateEventModel
    {
        [Required]
        public Guid Id { get; set; }
        [Required]
        public string? Title { get; set; } = string.Empty;
        [Required]
        public string? Description { get; set; } = string.Empty;
        [Required]
        public DateTime? Date { get; set; }
        [Required]
        public string Location { get; set; } = string.Empty;
    }

    public class UpdateEventCategoryModel
    {
        [Required]
        public Guid Id { get; set; }
        [Required]
        public Guid EventId { get; set; }
        [Required]
        public EventCategoryEnum Category { get; set; }
        [Required]
        public decimal Price { get; set; }
        [Required]
        public int Tiket { get; set; }
    }

}
