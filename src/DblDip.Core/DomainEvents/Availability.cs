using System;

namespace DblDip.Core.DomainEvents
{
    public record AvailabilityCreated(Guid AvailabilityId);
    public record AvailabilityRemoved(DateTime Deleted);
    public record AvailabilityUpdated();
}
