using BuildingBlocks.EventStore;
using System;
using System.Collections.Generic;

namespace DblDip.Core.DomainEvents
{
    public record AccountCreated(Guid AccountId, IList<Guid> ProfileIds, Guid DefaultProfileId, string Name, Guid AccountHolderUserId) : Event;
    public record AccountRemoved(DateTime Deleted): Event;
    public record AccountUpdated(string Value) : Event;
    public record SetDefaultProfile(Guid ProfileId) : Event;
    public record SetCurrentProfile(Guid ProfileId) : Event;
}
