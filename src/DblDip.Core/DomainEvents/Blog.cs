using DblDip.Core.Models;
using System;

namespace DblDip.Core.DomainEvents
{
    public record BlogCreated (Guid BlogId);
    public record BlogPostAdded (PostReference PostReference);
    public record BlogRemoved (DateTime Deleted);
}
