using System;

namespace DblDip.Domain.Features.FamilyPortraits
{
    public class FamilyPortraitDto
    {
        public Guid FamilyPortraitId { get; init; }
        public DateTime? Deleted { get; init; }
    }
}
