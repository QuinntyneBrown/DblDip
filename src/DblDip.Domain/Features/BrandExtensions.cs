using DblDip.Core.Models;
using DblDip.Domain.Features;

namespace DblDip.Domain.Features
{
    public static class BrandExtensions
    {
        public static BrandDto ToDto(this Brand brand)
            => new BrandDto(brand.BrandId);
    }
}
