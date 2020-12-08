using System;

namespace ShootQ.Core.DomainEvents
{
    public record UserCreated(Guid UserId, string Username, string Password, byte[] Salt);
    public record UserPasswordChanged(string Password);
    public record UserRoleAdded(string Name);
    public record UserRoleRemoved(string Name);
}
