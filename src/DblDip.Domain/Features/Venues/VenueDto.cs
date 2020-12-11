using System;

namespace DblDip.Domain.Features.Venues
{
    public class VenueDto
    {
        public Guid VenueId { get; private set; }
        public DateTime? Deleted { get; private set; }
    }
}
