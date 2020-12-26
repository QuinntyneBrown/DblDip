using System;

namespace DblDip.Core.DomainEvents
{
    public record PostRemoved (DateTime Deleted);
    public record PostPublished (DateTime Published);
    public record PostCreated (Guid PostId, Guid AuthorId, string Title);
    public record PostBodyUpdated (string Body);
    public record PostTitleUpdated (string Title);
}
