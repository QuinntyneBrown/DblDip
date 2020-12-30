using System;

namespace DblDip.Core.DomainEvents
{
    public record EpicCreated (Guid EpicId);
    public record EpicUpdated;
    public record EpicRemoved (DateTime Deleted);
}
