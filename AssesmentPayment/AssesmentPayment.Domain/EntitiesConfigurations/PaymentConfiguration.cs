using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssesmentPayment.Domain.EntitiesConfigurations
{
    public class PaymentConfiguration : BaseEntityConfiguration<Payment>
    {
        public override void EntityConfiguration(EntityTypeBuilder<Payment> builder)
        {
            builder.ToTable($"Tr{nameof(Payment)}");
            builder.HasKey(x => x.Id);
            builder.HasIndex(x => x.Id);
            builder.HasIndex(x => x.EventCategoryId);
            builder.HasIndex(x => x.UserId);
        }
    }
}
