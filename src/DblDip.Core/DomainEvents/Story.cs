using BuildingBlocks.EventStore;
using System;

namespace DblDip.Core.DomainEvents
{
    public record StoryCreated (Guid StoryId): Event;
    public record StoryUpdated: Event;
    public record StoryRemoved (DateTime Deleted): Event;
    public record StoryTaskAdded (Guid TaskId): Event;
    public record StoryTaskRemoved (Guid TaskId): Event;
}
