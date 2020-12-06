using ShootQ.Core.Models;
using System;
using System.Collections.Generic;

namespace ShootQ.Domain.Features.Weddings
{
    public class WeddingDto
    {
        public Guid WeddingId { get; set; }
        public ICollection<WeddingPart> Parts { get; set; }
            = new HashSet<WeddingPart>();
        public ICollection<Trip> Trips { get; set; }
        = new HashSet<Trip>();
    }


}
