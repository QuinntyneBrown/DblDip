using ShootQ.Core.DomainEvents;
using ShootQ.Core.ValueObjects;
using System;

namespace ShootQ.Core.Models
{
    public class Client : Profile
    {
        protected override void When(dynamic @event) => When(@event);

        public Client(string name, Email email)
            :base(new ProfileCreated(Guid.NewGuid(), name, email, nameof(Client), typeof(Client).AssemblyQualifiedName))
        {
            Apply(new ClientCreated(ProfileId));
        }
        public void When(ClientCreated clientCreated)
        {
            ClientId = clientCreated.ClientId;            
        }

        protected override void EnsureValidState()
        {

        }

        public Client CreateFrom(Lead lead)
        {
            throw new NotImplementedException("");
        }

        public Guid ClientId { get; private set; }

    }
}
