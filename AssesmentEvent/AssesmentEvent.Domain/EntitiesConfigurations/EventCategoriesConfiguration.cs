using AssesmentEvent.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssesmentEvent.Domain.EntitiesConfigurations
{
    public class EventCategoriesConfiguration : BaseEntityConfiguration<EventCategories>
    {
        public override void EntityConfiguration(EntityTypeBuilder<EventCategories> builder)
        {
            builder.ToTable($"Ms{nameof(EventCategories)}");
            builder.HasKey(x => x.Id);
            builder.HasIndex(x => x.Id);
            builder.HasIndex(x => x.EventId);
        }
    }
}
