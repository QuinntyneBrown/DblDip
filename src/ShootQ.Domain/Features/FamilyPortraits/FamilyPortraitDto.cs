using System;

namespace ShootQ.Domain.Features.FamilyPortraits
{
    public class FamilyPortraitDto
    {
        public Guid FamilyPortraitId { get; private set; }
        public DateTime? Deleted { get; private set; }
    }
}
