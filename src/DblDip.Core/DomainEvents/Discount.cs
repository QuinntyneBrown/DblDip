using System;

namespace DblDip.Core.DomainEvents
{
    public record DiscountCreated (Guid DiscountId);
    public record DiscountUpdated;
    public record DiscountRemoved (DateTime Deleted);
}
