using DblDip.Core.Models;
using DblDip.Domain.Features.Points;

namespace DblDip.Testing.Builders.Domain.Dtos
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
