using ShootQ.Core.ValueObjects;
using System;

namespace ShootQ.Core.DomainEvents
{
    public record RateCreated(Guid PhotographyRateId, Price Price, string Name);
}
