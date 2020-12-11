using BuildingBlocks.Abstractions;
using BuildingBlocks.Core;
using DblDip.Core.DomainEvents;
using DblDip.Core.Exceptions;
using DblDip.Core.ValueObjects;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;

namespace DblDip.Core.Models
{
    public class User : AggregateRoot
    {
        public User(string username, string password, IUsernameAvailabilityCheck usernameAvailabilityCheck = null, IPasswordHasher passwordHasher = null)
        {
            passwordHasher = new PasswordHasher();

            if (usernameAvailabilityCheck != null && usernameAvailabilityCheck.IsAvailable(username) == false)
                throw new DomainException("Email not available");

            var salt = new byte[128 / 8];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }
            var transformedPassword = passwordHasher.HashPassword(salt, password);

            Apply(new UserCreated(Guid.NewGuid(), username, transformedPassword, salt));
        }

        protected override void When(dynamic @event) => When(@event);

        protected void When(UserCreated userCreated)
        {
            UserId = userCreated.UserId;
            Username = (Email)userCreated.Username;
            Password = userCreated.Password;
            Salt = userCreated.Salt;
            Roles = new HashSet<RoleReference>();
        }

        protected void When(UserPasswordChanged userPasswordChanged)
        {
            Password = userPasswordChanged.Password;
        }

        protected void When(RoleReferenceAdded roleReferenceAdded)
        {
            Roles.Add(new RoleReference(roleReferenceAdded.RoleId, roleReferenceAdded.Name));
        }

        protected void When(RoleReferenceRemoved roleReferenceRemoved)
        {
            Roles.Remove(new RoleReference(roleReferenceRemoved.RoleId, roleReferenceRemoved.Name));
        }

        protected override void EnsureValidState()
        {

        }

        public void ChangePassword(string password)
        {
            Apply(new UserPasswordChanged(password));
        }

        public void AddRole(Guid roleId, string name)
        {
            Apply(new RoleReferenceAdded(roleId, name));
        }

        public void RemoveRole(Guid roleId, string value)
        {
            Apply(new RoleReferenceRemoved(roleId, value));
        }

        public Guid UserId { get; private set; }
        public Email Username { get; private set; }
        public string Password { get; private set; }
        public byte[] Salt { get; private set; }
        public ICollection<RoleReference> Roles { get; private set; }
        public DateTime? Deleted { get; private set; }

        public record RoleReference(Guid RoleId, string Name);
    }
}
