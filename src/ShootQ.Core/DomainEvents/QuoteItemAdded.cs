using ShootQ.Core.ValueObjects;

namespace ShootQ.Core.DomainEvents
{
    public record QuoteItemAdded(Price Amount, string Description);
}
