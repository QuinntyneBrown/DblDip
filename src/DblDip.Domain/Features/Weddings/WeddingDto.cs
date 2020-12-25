using DblDip.Core.Models;
using System;
using System.Collections.Generic;

namespace DblDip.Domain.Features.Weddings
{
    public class WeddingDto
    {
        public Guid WeddingId { get; init; }
        public ICollection<WeddingPart> Parts { get; init; }
            = new HashSet<WeddingPart>();
        public ICollection<Trip> Trips { get; init; }
        = new HashSet<Trip>();
    }


}
