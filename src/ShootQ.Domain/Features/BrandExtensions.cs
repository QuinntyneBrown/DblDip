using ShootQ.Core.Models;
using ShootQ.Domain.Features.Brands;

namespace ShootQ.Domain.Features
{
    public static class BrandExtensions
    {
        public static BrandDto ToDto(this Brand brand)
            => new BrandDto(brand.BrandId);
    }
}
