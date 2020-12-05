using ShootQ.Core.ValueObjects;
using System;

namespace ShootQ.Core.DomainEvents
{
    public record WeddingQuoteCreated(Email Email, Price Total, DateTime Created);
}
