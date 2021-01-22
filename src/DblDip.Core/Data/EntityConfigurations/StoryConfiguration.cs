using DblDip.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DblDip.Core.Data.EntityConfigurations
{
    public class StoryConfiguration : IEntityTypeConfiguration<Story>
    {
        public void Configure(EntityTypeBuilder<Story> builder)
        {
            builder.HasQueryFilter(p => !p.Deleted.HasValue);
        }
    }
}
