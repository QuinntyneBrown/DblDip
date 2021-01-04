using DblDip.Domain.Features;

namespace DblDip.Testing.Builders
{
    public class WeddingDtoBuilder
    {
        private WeddingDto _weddingDto;

        public static WeddingDto WithDefaults()
        {
            return new WeddingDto();
        }

        public WeddingDtoBuilder()
        {
            _weddingDto = new WeddingDto();
        }

        public WeddingDto Build()
        {
            return _weddingDto;
        }
    }
}
