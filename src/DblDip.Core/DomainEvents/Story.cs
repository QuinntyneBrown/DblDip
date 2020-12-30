using System;

namespace DblDip.Core.DomainEvents
{
    public record StoryCreated (Guid StoryId);
    public record StoryUpdated;
    public record StoryRemoved (DateTime Deleted);
}
