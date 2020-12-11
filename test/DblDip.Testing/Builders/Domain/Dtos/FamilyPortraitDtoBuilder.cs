using DblDip.Core.Models;
using DblDip.Domain.Features.FamilyPortraits;

namespace DblDip.Testing.Builders.Domain.Dtos
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
