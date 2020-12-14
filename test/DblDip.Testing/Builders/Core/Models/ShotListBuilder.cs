using DblDip.Core.Models;

namespace DblDip.Testing.Builders.Core.Models
{
    public class ShotListBuilder
    {
        private ShotList _shotList;

        public static ShotList WithDefaults()
        {
            return new ShotList();
        }

        public ShotListBuilder()
        {
            _shotList = WithDefaults();
        }

        public ShotList Build()
        {
            return _shotList;
        }
    }
}
