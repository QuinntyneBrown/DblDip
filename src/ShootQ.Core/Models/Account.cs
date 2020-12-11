using BuildingBlocks.Abstractions;
using ShootQ.Core.DomainEvents;
using System;
using System.Collections.Generic;

namespace ShootQ.Core.Models
{
    public class Account: AggregateRoot
    {
        protected override void When(dynamic @event) => When(@event);
        public Account(ICollection<Guid> profileIds, Guid defaultProfileId, string name, Guid accountHolderUserI)
        {
            Apply(new AccountCreated(Guid.NewGuid(), profileIds, defaultProfileId, name, AccountHolderUserId));
        }

        public void When(AccountCreated accountCreated)
        {
            AccountId = accountCreated.AccountId;
            ProfileIds = accountCreated.ProfileIds;
            DefaultProfileId = accountCreated.DefaultProfileId;
            Name = accountCreated.Name;
            AccountHolderUserId = accountCreated.AccountHolderUserId;
        }

        protected override void EnsureValidState()
        {

        }

        public Guid AccountId { get; private set; }
        public ICollection<Guid> ProfileIds { get; private set; }
        public Guid DefaultProfileId { get; private set; }
        public string Name { get; private set; }
        public Guid AccountHolderUserId { get; private set; }
        public DateTime? Deleted { get; private set; }
    }
}
