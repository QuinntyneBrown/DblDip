using BuildingBlocks.EventStore;
using System;

namespace DblDip.Core.DomainEvents
{
    public record DiscountCreated (Guid DiscountId): Event;
    public record DiscountUpdated: Event;
    public record DiscountRemoved (DateTime Deleted): Event;
}
