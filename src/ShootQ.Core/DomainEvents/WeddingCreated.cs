using ShootQ.Core.ValueObjects;
using System;

namespace ShootQ.Core.DomainEvents
{    
    public record WeddingCreated(Location Start, Location End, Location Location, Guid WeddingId, DateTime DateTime, int Hours, Guid PhotographyRateId);
}
