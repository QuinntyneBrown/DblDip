using DblDip.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DblDip.Core.Data
{
    public class YouTubeVideoConfiguration : IEntityTypeConfiguration<YouTubeVideo>
    {
        public void Configure(EntityTypeBuilder<YouTubeVideo> builder)
        {
            builder.HasQueryFilter(p => !p.Deleted.HasValue);
        }
    }
}
