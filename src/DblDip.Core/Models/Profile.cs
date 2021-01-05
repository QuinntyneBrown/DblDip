using BuildingBlocks.Abstractions;
using DblDip.Core.DomainEvents;
using DblDip.Core.ValueObjects;
using System;

namespace DblDip.Core.Models
{
    public class Profile : AggregateRoot
    {
        public Guid ProfileId { get; private set; }
        public Guid? DefaultDashboardId { get; private set; }
        public Guid AccountId { get; private set; }
        public Email Email { get; private set; }
        public PhoneNumber PhoneNumber { get; private set; }
        public Guid AvatarDigitalAssetId { get; private set; }
        public string Name { get; private set; }
        public string Firstname { get; private set; }
        public string Lastname { get; private set; }
        public string Type { get; private set; }
        public string DotNetType { get; private set; }
        public DateTime? Deleted { get; protected set; }

        public Profile(string name, Email email, System.Type type)
        {
            Apply(new ProfileCreated(Guid.NewGuid(), name, email, type.Name, type.AssemblyQualifiedName));
        }

        protected override void When(dynamic @event)
        {
            if (@event is AvatarUpdated || @event is ProfileCreated || @event is ProfileRemoved || @event is ProfileAccountIdUpdated || @event is ProfileNameUpdated)
            {
                When(@event);
            }
        }

        public void When(ProfileCreated profileCreated)
        {
            ProfileId = profileCreated.ProfileId;
            Name = profileCreated.Name;
            Email = profileCreated.Email;
            Type = profileCreated.Type;
            DotNetType = profileCreated.DotNetType;
        }

        public void When(AvatarUpdated avatarUpdated)
        {
            AvatarDigitalAssetId = avatarUpdated.AvatarDigitalAssetId;
        }

        public void When(ProfileRemoved profileRemoved)
        {
            Deleted = profileRemoved.Deleted;
        }

        public void When(ProfileAccountIdUpdated profileAccountIdUpdated)
        {
            AccountId = profileAccountIdUpdated.AccountId;
        }

        public void When(ProfileNameUpdated profileNameUpdated)
        {
            Name = profileNameUpdated.Name;
        }

        protected override void EnsureValidState()
        {

        }

        public void Remove(DateTime deleted)
        {
            Apply(new ProfileRemoved(deleted));
        }

        public void UpdateAccountId(Guid accountId)
        {
            Apply(new ProfileAccountIdUpdated(accountId));
        }

        public void UpdateAvatar(Guid avatarDigitalAssetId)
        {
            Apply(new AvatarUpdated(avatarDigitalAssetId));
        }

        public void UpdateName(string name)
        {
            Apply(new ProfileNameUpdated(name));
        }
    }
}
