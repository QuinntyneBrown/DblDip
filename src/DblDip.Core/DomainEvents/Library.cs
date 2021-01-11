using BuildingBlocks.EventStore;
using System;

namespace DblDip.Core.DomainEvents
{
    public record LibraryCreated (Guid LibraryId): Event;
    public record LibraryUpdated: Event;
    public record LibraryRemoved (DateTime Deleted): Event;
}
