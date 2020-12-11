using ShootQ.Core.Models;
using ShootQ.Core.ValueObjects;

namespace ShootQ.Testing.Builders.Core.Models
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
