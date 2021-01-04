using DblDip.Core.Models;
using DblDip.Domain.Features;

namespace DblDip.Testing.Builders
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
