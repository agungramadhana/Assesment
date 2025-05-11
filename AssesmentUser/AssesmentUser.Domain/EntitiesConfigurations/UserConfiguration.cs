using AssesmentUser.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Metadata.Conventions.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssesmentUser.Domain.EntitiesConfigurations
{
    public class UserConfiguration : BaseEntityConfiguration<User>
    {
        public override void EntityConfiguration(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("MsUser");
            builder.HasKey(x => x.Id);
            builder.HasIndex(x => x.Id);
        }
    }
}
