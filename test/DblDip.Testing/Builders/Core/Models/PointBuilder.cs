using DblDip.Core.Models;

namespace DblDip.Testing.Builders
{
    public class PointBuilder
    {
        private Point _point;

        public static Point WithDefaults()
        {
            return new Point();
        }

        public PointBuilder()
        {
            _point = WithDefaults();
        }

        public Point Build()
        {
            return _point;
        }
    }
}
