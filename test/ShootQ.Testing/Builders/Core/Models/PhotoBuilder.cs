using ShootQ.Core.Models;

namespace ShootQ.Testing.Builders.Core.Models
{
    public class PhotoBuilder
    {
        private Photo _photo;

        public static Photo WithDefaults()
        {
            return new Photo(default, default, default);
        }

        public PhotoBuilder()
        {
            _photo = WithDefaults();
        }

        public Photo Build()
        {
            return _photo;
        }
    }
}
