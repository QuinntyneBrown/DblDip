using System;

namespace DblDip.Core.DomainEvents
{
    public record MessageRemoved (DateTime Deleted);
    public record MessageCreated (Guid MessageId);
    public record MessageUpdated;
}
