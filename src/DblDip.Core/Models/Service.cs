using BuildingBlocks.Abstractions;
using DblDip.Core.DomainEvents;
using System;

namespace DblDip.Core.Models
{
    public class Service : AggregateRoot
    {
        protected override void When(dynamic @event)
        {
            if (@event is ServiceCreated || @event is ServiceRemoved || @event is ServiceUpdated)
            {
                When(@event);
            }
        }

        public Service()
        {

        }

        public Service(string name, Guid digitalAssetId, string description)
        {
            Apply(new ServiceCreated(Guid.NewGuid(), name, digitalAssetId, description));
        }
        public void When(ServiceCreated serviceCreated)
        {
            ServiceId = serviceCreated.ServiceId;
            Name = serviceCreated.Name;
            Description = serviceCreated.Description;
            DigitalAssetId = serviceCreated.DigitalAssetId;
        }

        public void When(ServiceUpdated serviceUpdated)
        {

        }

        public void When(ServiceRemoved serviceRemoved)
        {

        }

        protected override void EnsureValidState()
        {

        }


        public void Update(string value)
        {

        }

        public void Remove(DateTime deleted)
        {
            Apply(new ServiceRemoved(deleted));
        }

        public Guid ServiceId { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public Guid DigitalAssetId { get; private set; }
        public DateTime? Deleted { get; private set; }
    }
}
