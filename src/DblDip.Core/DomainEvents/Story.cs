using System;

namespace DblDip.Core.DomainEvents
{
    public record StoryCreated (Guid StoryId);
    public record StoryUpdated;
    public record StoryRemoved (DateTime Deleted);
    public record StoryTaskAdded (Guid TaskId);
    public record StoryTaskRemoved (Guid TaskId);
}
