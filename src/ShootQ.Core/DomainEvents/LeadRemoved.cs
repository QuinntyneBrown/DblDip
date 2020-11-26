using System;

namespace ShootQ.Core.DomainEvents
{
    public class LeadRemoved
    {
        public DateTime Deleted { get; } = DateTime.UtcNow;
    }
}
