using AssesmentEvent.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssesmentEvent.Application.Features
{
    public class CreateEventCategoryModel
    {
        [Required]
        public EventCategoryEnum Category { get; set; }
        [Required]
        public decimal Price { get; set; }
        [Required]
        public int Tiket { get; set; }
    }
}
