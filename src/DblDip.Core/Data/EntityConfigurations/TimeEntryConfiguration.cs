using DblDip.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DblDip.Core.Data.EntityConfigurations
{
    public class TimeEntryConfiguration : IEntityTypeConfiguration<TimeEntry>
    {
        public void Configure(EntityTypeBuilder<TimeEntry> builder)
        {
            builder.HasQueryFilter(p => !p.Deleted.HasValue);
        }
    }
}
