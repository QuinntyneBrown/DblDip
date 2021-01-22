using DblDip.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DblDip.Core.Data.EntityConfigurations
{
    public class PhotoStudioConfiguration : IEntityTypeConfiguration<PhotoStudio>
    {
        public void Configure(EntityTypeBuilder<PhotoStudio> builder)
        {
            builder.HasQueryFilter(p => !p.Deleted.HasValue);
        }
    }
}
