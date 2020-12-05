using ShootQ.Core.ValueObjects;
using System;

namespace ShootQ.Core.DomainEvents
{
    public record WeddingQuoteCreated(Guid WeddingQuoteId, Email Email, Guid WeddingId);

}
