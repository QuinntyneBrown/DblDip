using BuildingBlocks.Abstractions;
using BuildingBlocks.Core;
using ShootQ.Core.DomainEvents;
using ShootQ.Core.Exceptions;
using ShootQ.Core.ValueObjects;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;

namespace ShootQ.Core.Models
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

            Apply(new UserCreated(Guid.NewGuid(), username, transformedPassword, salt)) ;
        }

        protected override void When(dynamic @event) => When(@event);

        protected void When(UserCreated userCreated)
        {
            UserId = userCreated.UserId;
            Username = (Email)userCreated.Username;
            Password = userCreated.Password;
            Salt = userCreated.Salt;
            Roles = new HashSet<Role>();
        }

        protected void When(UserPasswordChanged userPasswordChanged)
        {
            Password = userPasswordChanged.Password;
        }

        protected void When(UserRoleAdded userRoleAdded)
        {
            Roles.Add(new Role(userRoleAdded.Name));
        }

        protected void When(UserRoleRemoved userRoleRemoved)
        {
            Roles.Remove(new Role(userRoleRemoved.Name));
        }

        protected override void EnsureValidState()
        {

        }

        public void ChangePassword(string password)
        {
            Apply(new UserPasswordChanged(password));
        }

        public void AddRole(string name)
        {
            Apply(new UserRoleAdded(name));
        }

        public void RemoveRole(string value)
        {
            Apply(new UserRoleRemoved(value));
        }

        public Guid UserId { get; private set; }
        public Email Username { get; private set; }
        public string Password { get; private set; }
        public byte[] Salt { get; private set; }
        public ICollection<Role> Roles { get; private set; }
        public DateTime? Deleted { get; private set; }

        public record Role(string Name);
    }
}
