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
        public Account(ICollection<Guid> profileIds, Guid defaultProfileId, string name, Guid userId)
        {
            Apply(new AccountCreated(Guid.NewGuid(), profileIds, defaultProfileId, name, userId));
        }

        private Account()
        {

        }

        public void When(AccountCreated accountCreated)
        {
            AccountId = accountCreated.AccountId;
            _profileIds = accountCreated.ProfileIds;
            DefaultProfileId = accountCreated.DefaultProfileId;
            Name = accountCreated.Name;
            UserId = accountCreated.AccountHolderUserId;
        }

        public void When(AccountRemoved accountRemoved)
        {
            Deleted = accountRemoved.Deleted;
        }

        public void When(AccountUpdated accountUpdated)
        {

        }

        public void When(SetDefaultProfile setDefaultProfile)
        {
            DefaultProfileId = setDefaultProfile.ProfileId;
        }

        public void When(SetCurrentProfile setCurrentProfile)
        {
            CurrentProfileId = setCurrentProfile.ProfileId;
        }
        protected override void EnsureValidState()
        {

        }

        public void Remove(DateTime deleted)
        {
            Apply(new AccountRemoved(deleted));
        }

        public void SetDefaultProfileId(Guid profileId)
        {
            DefaultProfileId = profileId;
        }

        public void SetCurrentProfileId(Guid profileId)
        {
            CurrentProfileId = profileId;
        }

        public void Update()
        {

        }

        private ICollection<Guid> _profileIds;
        public Guid AccountId { get; private set; }
        public IReadOnlyList<Guid> ProfileIds => _profileIds.ToList();
        public Guid DefaultProfileId { get; private set; }
        public Guid CurrentProfileId { get; set; }
        public string Name { get; private set; }
        public Guid UserId { get; private set; }
        public DateTime? Deleted { get; private set; }
    }
}
