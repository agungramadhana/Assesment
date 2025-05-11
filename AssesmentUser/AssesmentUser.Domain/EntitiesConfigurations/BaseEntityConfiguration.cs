using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssesmentUser.Domain.EntitiesConfigurations
{
    public abstract class BaseEntityConfiguration<TBaseEntity> : IEntityTypeConfiguration<TBaseEntity> where TBaseEntity : BaseEntity
    {
        public void Configure(EntityTypeBuilder<TBaseEntity> builder)
        {
            builder.Property(p => p.CreatedBy).HasMaxLength(maxLength: 100);

            builder.Property(p => p.UpdatedBy).HasMaxLength(maxLength: 100);

            EntityConfiguration(builder);
        }

        public abstract void EntityConfiguration(EntityTypeBuilder<TBaseEntity> builder);
    }
}
