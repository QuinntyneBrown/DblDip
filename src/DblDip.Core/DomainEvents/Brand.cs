using System;

namespace DblDip.Core.DomainEvents
{
    public record BrandCreated (Guid BrandId);
    public record BrandRemoved (DateTime Deleted);
    public record BrandUpdated (string Value);
}
