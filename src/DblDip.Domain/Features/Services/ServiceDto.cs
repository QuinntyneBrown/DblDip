using System;

namespace DblDip.Domain.Features.Services
{
    public class ServiceDto
    {
        public Guid ServiceId { get; init; }
        public DateTime? Deleted { get; init; }
    }
}
