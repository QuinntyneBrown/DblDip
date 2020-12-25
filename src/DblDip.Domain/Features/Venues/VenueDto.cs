using System;

namespace DblDip.Domain.Features.Venues
{
    public class VenueDto
    {
        public Guid VenueId { get; init; }
        public DateTime? Deleted { get; init; }
    }
}
