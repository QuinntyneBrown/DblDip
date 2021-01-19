using BuildingBlocks.EventStore;
using DblDip.Core.DomainEvents;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DblDip.Core.Models
{
    public class Account : AggregateRoot
    {
        public Guid AccountId { get; private set; }
        public Guid DefaultProfileId { get; private set; }
        public Guid CurrentProfileId { get; private set; }
        public string Name { get; private set; }
        public Guid UserId { get; private set; }
        public List<ProfileReference> Profiles => _profiles;
        public DateTime? Deleted { get; private set; }
        
        private List<ProfileReference> _profiles;
        public Account(Guid profileId, string name, Guid userId)
        {
            Apply(new AccountCreated(Guid.NewGuid(), new List<Guid> { profileId }, profileId, name, userId));
        }

        private Account()
        {

        }

        protected override void When(dynamic @event) => When(@event);

        public void When(AccountCreated accountCreated)
        {
            AccountId = accountCreated.AccountId;
            _profiles = accountCreated.ProfileIds.Select(x => new ProfileReference(x)).ToList();
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
    }
}
