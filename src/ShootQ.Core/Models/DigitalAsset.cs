using BuildingBlocks.Abstractions;
using ShootQ.Core.DomainEvents;
using System;

namespace ShootQ.Core.Models
{
    public class DigitalAsset : AggregateRoot
    {
        public DigitalAsset(string name, byte[] bytes, string contentType)
        {
            Apply(new DigitalAssetCreated(Guid.NewGuid(), name, bytes, contentType));
        }
        protected override void When(dynamic @event) => When(@event);

        public void When(DigitalAssetCreated digitalAssetCreated)
        {
            DigitalAssetId = digitalAssetCreated.DigitalAssetId;
            Name = digitalAssetCreated.Name;
            Bytes = digitalAssetCreated.Bytes;
            ContentType = digitalAssetCreated.ContentType;
        }

        protected override void EnsureValidState()
        {
            
        }

        public Guid DigitalAssetId { get; private set; }
        public string Name { get; private set; }
        public byte[] Bytes { get; private set; }
        public string ContentType { get; private set; }
    }
}
