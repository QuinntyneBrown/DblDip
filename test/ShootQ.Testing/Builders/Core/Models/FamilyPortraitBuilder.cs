using ShootQ.Core.Models;

namespace ShootQ.Testing.Builders.Core.Models
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
