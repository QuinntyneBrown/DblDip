using System;

namespace ShootQ.Core.DomainEvents
{
    public record LeadCreated(Guid LeadId);
    public record LeadRemoved(DateTime Deleted);
}
