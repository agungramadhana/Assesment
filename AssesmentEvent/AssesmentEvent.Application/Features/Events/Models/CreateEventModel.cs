using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssesmentEvent.Application.Features
{
    public class CreateEventModel
    {
        [Required]
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; } = string.Empty;
        [Required]
        public DateTime Date { get; set; }
        [Required]
        public string Location { get; set; } = string.Empty;
        [Required]
        public List<CreateEventCategoryModel> Categories { get; set; } = new List<CreateEventCategoryModel>();
    }
}
