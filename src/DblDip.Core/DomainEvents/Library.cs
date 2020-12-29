using System;

namespace DblDip.Core.DomainEvents
{
    public record LibraryCreated (Guid LibraryId);
    public record LibraryUpdated;
    public record LibraryRemoved (DateTime Deleted);
}
