using DblDip.Core.ValueObjects;
using System;

namespace DblDip.Core.DomainEvents
{
    public record RateCreated(Guid PhotographyRateId, Price Price, string Name);
    public record RateUpdated (string Value);
    public record RateRemoved (DateTime Deleted);
}
