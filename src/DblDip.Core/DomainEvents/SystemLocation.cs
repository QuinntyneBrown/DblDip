using DblDip.Core.ValueObjects;
using System;

namespace DblDip.Core.DomainEvents
{
    public record SystemLocationCreated(Guid SystemLocationId, string Name, Location Location);
    public record SystemLocationRemoved(DateTime Deleted);
}
