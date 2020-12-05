using ShootQ.Core.ValueObjects;
using System;

namespace ShootQ.Core.DomainEvents
{    
    public record WeddingCreated(Location Start, Location End, Location Location, Guid WeddingId, DateTime DateTime, int Hours, Guid PhotographyRateId);

    public record WeddingPartAdded(DateTime DateTime, int Hours, Location Location, Guid PhotographyRateId, string Description);
}
