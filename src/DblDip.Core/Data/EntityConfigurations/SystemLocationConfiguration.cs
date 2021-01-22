using DblDip.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DblDip.Core.Data.EntityConfigurations
{
    public class SystemLocationConfiguration : IEntityTypeConfiguration<SystemLocation>
    {
        public void Configure(EntityTypeBuilder<SystemLocation> builder)
        {
            builder.HasQueryFilter(p => !p.Deleted.HasValue);
        }
    }
}
