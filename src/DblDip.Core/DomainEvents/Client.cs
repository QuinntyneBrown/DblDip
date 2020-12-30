using System;

namespace DblDip.Core.DomainEvents
{
    public record ClientCreated(Guid ClientId);
    public record ClientUpdated;
}
