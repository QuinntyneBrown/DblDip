using BuildingBlocks.EventStore;
using System;

namespace DblDip.Core.DomainEvents
{
    public record SystemAdministratorCreated(Guid SystemAdministratorId): Event;
    public record SystemAdministratorUpdated: Event;
}
