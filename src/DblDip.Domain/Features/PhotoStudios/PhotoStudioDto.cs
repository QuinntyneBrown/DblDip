using BuildingBlocks.GeoLocation;
using System;

namespace DblDip.Domain.Features
{
    public class PhotoStudioDto
    {
        public Location Location { get; init; }
        public Guid PhotoStudioId { get; init; }
    }
}
