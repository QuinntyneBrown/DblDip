using BuildingBlocks.Abstractions;
using ShootQ.Core.DomainEvents;
using ShootQ.Core.ValueObjects;
using System;

namespace ShootQ.Core.Models
{
    public abstract class Profile: AggregateRoot
    {        
        public Profile(ProfileCreated profileCreated)
        {
            Apply(profileCreated);
        }
        public void When(ProfileCreated profileCreated)
        {
            ProfileId = profileCreated.ProfileId;
            Name = profileCreated.Name;
            Email = profileCreated.Email;
        }

        protected override void EnsureValidState()
        {

        }


        public Guid AccountId { get; private set; }
        public Guid ProfileId { get; private set; }
        public Email Email { get; set; }
        public PhoneNumber PhoneNumber { get; set; }
        public Guid AvatarDigitalAssetId { get; private set; }
        public string Name { get; private set; }
        public string Firstname { get; private set; }
        public string Lastname { get; private set; }
        public string DotNetType { get; set; }
        public DateTime? Deleted { get; private set; }
    }
}
