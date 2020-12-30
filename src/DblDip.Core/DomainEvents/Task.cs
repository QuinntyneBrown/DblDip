using System;

namespace DblDip.Core.DomainEvents
{
    public record TaskCreated (Guid TaskId);
    public record TaskUpdated;
    public record TaskRemoved (DateTime Deleted);
}
