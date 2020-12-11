using System;

namespace ShootQ.Domain.Features.Brands
{
    public class BrandDto
    {
        public Guid BrandId { get; private set; }
        public DateTime? Deleted { get; private set; }
    }
}
