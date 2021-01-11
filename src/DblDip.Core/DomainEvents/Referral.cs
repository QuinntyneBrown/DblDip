using BuildingBlocks.EventStore;
using System;

namespace DblDip.Core.DomainEvents
{
    public record ReferralCreated (Guid ReferralId): Event;
    public record ReferralUpdated: Event;
    public record ReferralRemoved (DateTime Deleted): Event;
}
