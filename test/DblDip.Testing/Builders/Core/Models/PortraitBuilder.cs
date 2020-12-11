using DblDip.Core.Models;

namespace DblDip.Testing.Builders.Core.Models
{
    public class PortraitBuilder
    {
        private Portrait _portrait;

        public static Portrait WithDefaults()
        {
            return new Portrait();
        }

        public PortraitBuilder()
        {
            _portrait = WithDefaults();
        }

        public Portrait Build()
        {
            return _portrait;
        }
    }
}
