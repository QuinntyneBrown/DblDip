using System;

namespace DblDip.Core.DomainEvents
{
    public record CardCreated(Guid CardId, string Name, string Description);
    public record CardRemoved(DateTime Deleted);
    public record CardUpdated;
}
