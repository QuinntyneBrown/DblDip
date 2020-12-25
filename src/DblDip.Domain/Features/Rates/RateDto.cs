using DblDip.Core.ValueObjects;
using System;

namespace DblDip.Domain.Features.Rates
{
    public class RateDto
    {
        public Guid RateId { get; init; }
        public string Name { get; init; }
        public Price Price { get; init; }
        public DateTime? Deleted { get; init; }
    }
}
