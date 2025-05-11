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
    public class EventRegistrationConfiguration : BaseEntityConfiguration<EventRegistration>
    {
        public override void EntityConfiguration(EntityTypeBuilder<EventRegistration> builder)
        {
            builder.ToTable($"Tr{nameof(EventRegistration)}");
            builder.HasKey(x => x.Id);
            builder.HasIndex(x => x.Id);
            builder.HasIndex(x => x.EventCategoryId);
            builder.HasIndex(x => x.UserId);
        }
    }
}
