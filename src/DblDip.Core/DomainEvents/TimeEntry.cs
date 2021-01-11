using BuildingBlocks.EventStore;
using System;

namespace DblDip.Core.DomainEvents
{
    public record TimeEntryRemoved (DateTime Deleted): Event;
    public record TimeEntryCreated (Guid TimeEntryId): Event;
    public record TimeEntryUpdated: Event;
}
