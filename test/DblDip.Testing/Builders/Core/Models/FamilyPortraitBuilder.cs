using DblDip.Core.Models;

namespace DblDip.Testing.Builders
{
    public class FamilyPortraitBuilder
    {
        private FamilyPortrait _familyPortrait;

        public static FamilyPortrait WithDefaults()
        {
            return new FamilyPortrait();
        }

        public FamilyPortraitBuilder()
        {
            _familyPortrait = WithDefaults();
        }

        public FamilyPortrait Build()
        {
            return _familyPortrait;
        }
    }
}
