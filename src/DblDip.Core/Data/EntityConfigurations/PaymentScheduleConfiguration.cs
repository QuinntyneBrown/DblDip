using DblDip.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DblDip.Core.Data.EntityConfigurations
{
    public class PaymentScheduleConfiguration : IEntityTypeConfiguration<PaymentSchedule>
    {
        public void Configure(EntityTypeBuilder<PaymentSchedule> builder)
        {
            builder.HasQueryFilter(p => !p.Deleted.HasValue);
        }
    }
}
