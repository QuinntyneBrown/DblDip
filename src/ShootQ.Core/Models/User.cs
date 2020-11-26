using BuildingBlocks.Abstractions;
using ShootQ.Core.DomainEvents;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;

namespace ShootQ.Core.Models
{
    public class User: AggregateRoot
    {
        public User(string username, string password)
        {
            var salt = new byte[128 / 8];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }

            Apply(new UserCreated(username, password, salt));
        }

        protected override void When(dynamic @event) => When(@event);

        protected void When(UserCreated userCreated)
        {
            UserId = userCreated.UserId;
            Username = userCreated.Username;
            Password = userCreated.Password;
            Salt = userCreated.Salt;
            Roles = new HashSet<Role>();
        }

        protected void When(UserPasswordChanged userPasswordChanged)
        {
            Password = userPasswordChanged.Password;
        }

        protected void When(RoleAdded roleAdded)
        {
            Roles.Add(new Role(roleAdded.Name));
        }

        protected void When(RoleRemoved roleRemoved)
        {
            Roles.Remove(new Role(roleRemoved.Name));
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
            Apply(new RoleAdded(name));
        }

        public void RemoveRole(string value)
        {
            Apply(new RoleRemoved(value));
        }

        public Guid UserId { get; private set; }
        public string Username { get; private set; }
        public string Password { get; private set; }
        public byte[] Salt { get; private set; }
        public ICollection<Role> Roles { get; private set; }
        public DateTime? Deleted { get; private set; }

        public record Role
        {
            public Role(string name)
            {
                Name = name;
            }

            public string Name { get; private set; }
        }
    }
}
