using System;

namespace DblDip.Core.DomainEvents
{
    public record OfferCreated (Guid OfferId);
    public record OfferRemoved (DateTime Deleted);
}
