using System;

namespace ShootQ.Domain.Features.Services
{
    public class ServiceDto
    {
        public Guid ServiceId { get; private set; }
        public DateTime? Deleted { get; private set; }
    }
}
