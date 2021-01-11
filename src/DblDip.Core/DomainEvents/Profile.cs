using BuildingBlocks.EventStore;
using DblDip.Core.ValueObjects;
using System;

namespace DblDip.Core.DomainEvents
{
    public record ProfileCreated(Guid ProfileId, string Name, Email Email, string Type, string DotNetType): Event;
    public record ProfileAvatarAdded(string Value): Event;
    public record ProfileRemoved(DateTime Deleted): Event;
    public record ProfileAccountIdUpdated (Guid AccountId): Event;
    public record ProfileNameUpdated (string Name): Event;
}
