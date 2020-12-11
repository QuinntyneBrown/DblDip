using ShootQ.Core.Models;
using ShootQ.Domain.Features.Brands;

namespace ShootQ.Testing.Builders.Domain.Dtos
{
    public class BrandDtoBuilder
    {
        private BrandDto _brandDto;

        public static BrandDto WithDefaults()
        {
            return new BrandDto(default);
        }

        public BrandDtoBuilder()
        {
            _brandDto = WithDefaults();
        }

        public BrandDto Build()
        {
            return _brandDto;
        }
    }
}
