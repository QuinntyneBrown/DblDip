using System;

namespace DblDip.Core.DomainEvents
{
    public record PhotoStudioCreated (Guid PhotoStudioId);
    public record PhotoStudioUpdated;
    public record PhotoStudioRemoved (DateTime Deleted);
}
