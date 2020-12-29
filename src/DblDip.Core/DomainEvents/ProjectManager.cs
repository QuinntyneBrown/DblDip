using System;

namespace DblDip.Core.DomainEvents
{
    public record ProjectManagerCreated(Guid ProjectManagerId);
    public record ProjectManagerUpdated;
}
