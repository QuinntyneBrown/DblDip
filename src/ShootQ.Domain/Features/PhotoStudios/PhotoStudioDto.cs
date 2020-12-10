using BuildingBlocks.GeoLocation;
using System;

namespace ShootQ.Domain.Features.PhotoStudios
{
    public class PhotoStudioDto
    {
        public Location Location { get; set; }
        public Guid PhotoStudioId { get; set; }
    }
}
