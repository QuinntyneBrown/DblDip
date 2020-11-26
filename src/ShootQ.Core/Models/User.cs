using BuildingBlocks.Abstractions;
using ShootQ.Core.DomainEvents;
using System;
using System.Collections.Generic;

namespace ShootQ.Core.Models
{
    public class User: AggregateRoot
    {
        public User(string username, string password)
        {
            Apply(new UserCreated(username, password));
        }

        protected override void When(dynamic @event) => When(@event);

        protected void When(UserCreated userCreated)
        {
            Salt = userCreated.Salt;
            Username = userCreated.Username;
            Password = userCreated.Password;
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
