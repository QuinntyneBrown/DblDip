using System;

namespace DblDip.Domain.Features
{
    public class ServiceDto
    {
        public Guid ServiceId { get; init; }
        public DateTime? Deleted { get; init; }
    }
}
