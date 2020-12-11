using DblDip.Core.Models;
using DblDip.Domain.Features.Brands;

namespace DblDip.Testing.Builders.Domain.Dtos
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
