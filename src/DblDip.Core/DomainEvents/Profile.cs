using DblDip.Core.ValueObjects;
using System;

namespace DblDip.Core.DomainEvents
{
    public record ProfileCreated(Guid ProfileId, string Name, Email Email, string Type, string DotNetType);
    public record ProfileAvatarAdded(string Value);
    public record ProfileRemoved(DateTime Deleted);
    public record ProfileAccountIdUpdated (Guid AccountId);
    public record ProfileNameUpdated (string Name);
}
