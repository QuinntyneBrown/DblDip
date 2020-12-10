using ShootQ.Core.ValueObjects;
using System;

namespace ShootQ.Domain.Features.Rates
{
    public class RateDto
    {
        public Guid RateId { get; set; }
        public string Name { get; set; }
        public Price Price { get; set; }
        public DateTime? Deleted { get; set; }
    }
}
