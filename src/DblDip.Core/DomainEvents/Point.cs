using System;

namespace DblDip.Core.DomainEvents
{
    public record PointCreated (Guid PointId);
    public record PointUpdated;
    public record PointRemoved (DateTime Deleted);
}
