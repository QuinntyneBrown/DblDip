using System;

namespace DblDip.Core.DomainEvents
{
    public record ReferralCreated (Guid ReferralId);
    public record ReferralUpdated;
    public record ReferralRemoved (DateTime Deleted);
}
