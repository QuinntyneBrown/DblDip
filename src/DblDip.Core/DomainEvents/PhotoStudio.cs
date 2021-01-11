using BuildingBlocks.EventStore;
using System;

namespace DblDip.Core.DomainEvents
{
    public record PhotoStudioCreated (Guid PhotoStudioId): Event;
    public record PhotoStudioUpdated: Event;
    public record PhotoStudioRemoved (DateTime Deleted): Event;
}
