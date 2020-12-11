using DblDip.Core.ValueObjects;
using System;

namespace DblDip.Domain.Features.SystemLocations
{
    public class SystemLocationDto
    {
        public Guid SystemLocationId { get; set; }
        public string Name { get; set; }
        public Location Location { get; set; }
    }
}
