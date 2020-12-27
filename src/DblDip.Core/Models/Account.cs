using BuildingBlocks.Abstractions;
using DblDip.Core.DomainEvents;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DblDip.Core.Models
{
    public class Account : AggregateRoot
    {
        protected override void When(dynamic @event) => When(@event);
        public Account(ICollection<ProfileReference> profiles, Guid defaultProfileId, string name, Guid accountHolderUserI)
        {
            Apply(new AccountCreated(Guid.NewGuid(), profiles, defaultProfileId, name, AccountHolderUserId));
        }

        public void When(AccountCreated accountCreated)
        {
            AccountId = accountCreated.AccountId;
            _profiles = accountCreated.Profiles;
            DefaultProfileId = accountCreated.DefaultProfileId;
            Name = accountCreated.Name;
            AccountHolderUserId = accountCreated.AccountHolderUserId;
        }

        public void When(AccountRemoved accountRemoved)
        {
            Deleted = accountRemoved.Deleted;
        }

        public void When(AccountUpdated accountUpdated)
        {

        }

        protected override void EnsureValidState()
        {

        }

        public void Remove(DateTime deleted)
        {
            Apply(new AccountRemoved(deleted));
        }

        public void Update()
        {
            
        }

        private ICollection<ProfileReference> _profiles;
        public Guid AccountId { get; private set; }
        public IReadOnlyList<ProfileReference> Profiles => _profiles.ToList();
        public Guid DefaultProfileId { get; private set; }
        public string Name { get; private set; }
        public Guid AccountHolderUserId { get; private set; }
        public DateTime? Deleted { get; private set; }
    }
}
