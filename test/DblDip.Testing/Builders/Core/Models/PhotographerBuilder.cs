using DblDip.Core.Models;
using DblDip.Core.ValueObjects;

namespace DblDip.Testing.Builders
{
    public class PhotographerBuilder
    {
        private Photographer _photographer;

        public static Photographer WithDefaults()
        {
            return new Photographer("Test", (Email)"test@test.com");
        }

        public PhotographerBuilder()
        {
            _photographer = WithDefaults();
        }

        public Photographer Build()
        {
            return _photographer;
        }
    }
}
