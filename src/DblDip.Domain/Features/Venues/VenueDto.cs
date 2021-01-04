using System;

namespace DblDip.Domain.Features
{
    public class VenueDto
    {
        public Guid VenueId { get; init; }
        public DateTime? Deleted { get; init; }
    }
}
