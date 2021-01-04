using DblDip.Core.Models;
using DblDip.Domain.Features;

namespace DblDip.Testing.Builders
{
    public class PointDtoBuilder
    {
        private PointDto _pointDto;

        public static PointDto WithDefaults()
        {
            return new PointDto();
        }

        public PointDtoBuilder()
        {
            _pointDto = WithDefaults();
        }

        public PointDto Build()
        {
            return _pointDto;
        }
    }
}
