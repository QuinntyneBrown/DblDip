using DblDip.Core.ValueObjects;
using System;

namespace DblDip.Core.DomainEvents
{
    public record ProfileCreated (Guid ProfileId, string Name, Email Email, string Type, string DotNetType);
    public record ProfileAvatarAdded(string Value);
}
