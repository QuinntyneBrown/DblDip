using ShootQ.Core.Models;
using ShootQ.Domain.Features.FamilyPortraits;

namespace ShootQ.Testing.Builders.Domain.Dtos
{
    public class FamilyPortraitDtoBuilder
    {
        private FamilyPortraitDto _familyPortraitDto;

        public static FamilyPortraitDto WithDefaults()
        {
            return new FamilyPortraitDto();
        }

        public FamilyPortraitDtoBuilder()
        {
            _familyPortraitDto = WithDefaults();
        }

        public FamilyPortraitDto Build()
        {
            return _familyPortraitDto;
        }
    }
}
