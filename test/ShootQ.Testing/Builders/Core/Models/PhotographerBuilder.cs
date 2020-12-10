using ShootQ.Core.Models;

namespace ShootQ.Testing.Builders.Core.Models
{
    public class PhotographerBuilder
    {
        private Photographer _photographer;

        public static Photographer WithDefaults()
        {
            return new Photographer();
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
