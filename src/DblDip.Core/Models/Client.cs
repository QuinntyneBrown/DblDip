using DblDip.Core.DomainEvents;
using DblDip.Core.ValueObjects;
using System;

namespace DblDip.Core.Models
{
    public class Client : Profile
    {
        public Guid ClientId { get; private set; }
        protected override void When(dynamic @event) => When(@event);

        public Client(string name, Email email)
            : base(name, email, typeof(Client))
        {
            Apply(new ClientCreated(ProfileId));
        }
        public void When(ClientCreated clientCreated)
        {
            ClientId = clientCreated.ClientId;
        }

        public void When(ClientUpdated clientUpdated)
        {

        }

        protected override void EnsureValidState()
        {

        }

        public Client CreateFrom(Lead lead)
        {
            throw new NotImplementedException("");
        }

        public void Update()
        {
            Apply(new ClientUpdated());
        }
    }
}
