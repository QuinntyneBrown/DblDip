using ShootQ.Core.Models;
using ShootQ.Core.ValueObjects;
using System;
using System.Collections.Generic;

namespace ShootQ.Domain.Features.Weddings
{
    public class WeddingDto
    {
        public Guid WeddingId { get; set; }
        public Guid CustomerId { get; set; }
        public ICollection<WeddingPart> Parts { get; set; }
            = new HashSet<WeddingPart>();

        public Price Total { get; set; }
    }
}
