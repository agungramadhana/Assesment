using AssesmentEvent.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Metadata.Conventions.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssesmentEvent.Domain.EntitiesConfigurations
{
    public class EventConfiguration : BaseEntityConfiguration<Event>
    {
        public override void EntityConfiguration(EntityTypeBuilder<Event> builder)
        {
            builder.ToTable($"Ms{nameof(Event)}");
            builder.HasKey(x => x.Id);
            builder.HasIndex(x => x.Id);
        }
    }
}
