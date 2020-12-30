using System;

namespace DblDip.Core.DomainEvents
{
    public record BlogCreated(Guid BlogId);
    public record BlogPostAdded(Guid PostId, string Title);
    public record BlogRemoved(DateTime Deleted);
    public record BlogUpdated;
}
