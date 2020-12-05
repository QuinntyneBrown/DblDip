using ShootQ.Core.ValueObjects;
using System;

namespace ShootQ.Core.DomainEvents
{
    public record PhotographyRateCreated(Guid PhotographyRateId, Price Price);
}
