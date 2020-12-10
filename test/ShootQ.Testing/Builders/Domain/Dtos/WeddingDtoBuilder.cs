using ShootQ.Domain.Features.Weddings;

namespace ShootQ.Testing.Builders.Domain.Dtos
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
