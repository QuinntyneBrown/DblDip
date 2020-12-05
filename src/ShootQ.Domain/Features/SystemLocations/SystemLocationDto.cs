using ShootQ.Core.ValueObjects;
using System;

namespace ShootQ.Domain.Features.SystemLocations
{
    public class SystemLocationDto
    {
        public Guid SystemLocationId { get; set; }
        public string Name { get; set; }
        public Location Location { get; set; }
    }
}
