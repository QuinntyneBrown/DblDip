using DblDip.Core.ValueObjects;
using System;

namespace DblDip.Core.DomainEvents
{
    public record RateCreated(Guid PhotographyRateId, Price Price, string Name);
}
