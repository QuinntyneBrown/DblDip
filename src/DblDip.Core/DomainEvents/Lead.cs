using System;

namespace DblDip.Core.DomainEvents
{
    public record LeadCreated(Guid LeadId);
    public record LeadRemoved(DateTime Deleted);
}
