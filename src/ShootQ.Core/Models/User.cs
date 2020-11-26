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

        public void When(UserCreated userCreated)
        {
            Salt = userCreated.Salt;
            Username = userCreated.Username;
            Password = userCreated.Password;
            Roles = new HashSet<Role>();
        }

        public void When(UserPasswordChanged userPasswordChanged)
        {
            Password = userPasswordChanged.Password;
        }

        protected override void EnsureValidState()
        {

        }

        public void ChangePassword(string password)
        {
            Apply(new UserPasswordChanged(password));
        }

        public Guid UserId { get; private set; }
        public string Username { get; private set; }
        public string Password { get; private set; }
        public byte[] Salt { get; private set; }
        public ICollection<Role> Roles { get; private set; }
        public DateTime? Deleted { get; private set; }

    }
}
