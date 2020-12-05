using System;

namespace ShootQ.Core.DomainEvents
{    
    public record WeddingCreated(Guid WeddingId, DateTime DateTime, int Hours, Guid CustomerId, Guid PhotographyRateId);
}
