using System;

namespace DblDip.Core.DomainEvents
{
    public record TaskCreated (Guid TaskId, Guid OwnerId, string Description);
    public record TaskUpdated;
    public record TaskRemoved (DateTime Deleted);
    public record TaskCompleted (DateTime Completed);
}
