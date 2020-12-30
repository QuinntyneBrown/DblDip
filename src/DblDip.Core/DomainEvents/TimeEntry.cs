using System;

namespace DblDip.Core.DomainEvents
{
    public record TimeEntryRemoved (DateTime Deleted);
    public record TimeEntryCreated (Guid TimeEntryId);
    public record TimeEntryUpdated;
}
