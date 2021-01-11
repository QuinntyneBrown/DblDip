using BuildingBlocks.EventStore;
using System;

namespace DblDip.Core.DomainEvents
{
    public record UserCreated(Guid UserId, string Username, string Password, byte[] Salt, bool PasswordResetRequired): Event;
    public record UserPasswordChanged(string Password): Event;
    public record RoleReferenceAdded(Guid RoleId, string Name): Event;
    public record RoleReferenceRemoved(Guid RoleId, string Name): Event;
    public record UserRefreshTokenAdded(string RefreshToken): Event;
}
