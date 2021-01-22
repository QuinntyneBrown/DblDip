using DblDip.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DblDip.Core.Data.EntityConfigurations
{
    public class PhotographyProjectConfiguration : IEntityTypeConfiguration<PhotographyProject>
    {
        public void Configure(EntityTypeBuilder<PhotographyProject> builder)
        {
            builder.HasQueryFilter(p => !p.Deleted.HasValue);
        }
    }
}
