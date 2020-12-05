using ShootQ.Core.ValueObjects;
using System;

namespace ShootQ.Core.DomainEvents
{
    public record WeddingRateCreated(Guid WeddingRateId, Price Price);
}
