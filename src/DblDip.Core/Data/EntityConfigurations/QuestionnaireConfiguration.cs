using DblDip.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DblDip.Core.Data.EntityConfigurations
{
    public class QuestionnaireConfiguration : IEntityTypeConfiguration<Questionnaire>
    {
        public void Configure(EntityTypeBuilder<Questionnaire> builder)
        {
            builder.HasQueryFilter(p => !p.Deleted.HasValue);
        }
    }
}
