using DblDip.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DblDip.Core.Data.EntityConfigurations
{
    public class PhotoGalleryConfiguration : IEntityTypeConfiguration<PhotoGallery>
    {
        public void Configure(EntityTypeBuilder<PhotoGallery> builder)
        {
            builder.HasQueryFilter(p => !p.Deleted.HasValue);
        }
    }
}
