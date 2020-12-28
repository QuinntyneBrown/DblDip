using System;

namespace DblDip.Core.DomainEvents
{
    public record ContactCreated(Guid ContactId);
    public record ContactRemoved(DateTime Deleted);
    public record ContactUpdated();
}
