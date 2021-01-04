using DblDip.Domain.Features;

namespace DblDip.Testing.Builders
{
    public class ShotListDtoBuilder
    {
        private ShotListDto _shotListDto;

        public static ShotListDto WithDefaults()
        {
            return new ShotListDto();
        }

        public ShotListDtoBuilder()
        {
            _shotListDto = WithDefaults();
        }

        public ShotListDto Build()
        {
            return _shotListDto;
        }
    }
}
