using DblDip.Core.ValueObjects;
using System;

namespace DblDip.Domain.Features.SystemLocations
{
    public class SystemLocationDto
    {
        public Guid SystemLocationId { get; init; }
        public string Name { get; init; }
        public Location Location { get; init; }
    }
}
