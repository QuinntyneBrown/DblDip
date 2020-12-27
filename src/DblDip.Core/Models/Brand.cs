using BuildingBlocks.Abstractions;
using DblDip.Core.DomainEvents;
using System;

namespace DblDip.Core.Models
{
    public class Brand : AggregateRoot
    {
        protected override void When(dynamic @event) => When(@event);

        public Brand()
        {
            Apply(new BrandCreated(Guid.NewGuid()));
        }

        public void When(BrandCreated brandCreated)
        {
            BrandId = brandCreated.BrandId;
        }

        public void When(BrandRemoved brandRemoved)
        {
            Deleted = brandRemoved.Deleted;
        }

        public void When(BrandUpdated brandUpdated)
        {

        }

        protected override void EnsureValidState()
        {

        }

        public void Remove(DateTime deleted)
        {
            Apply(new BrandRemoved(deleted));
        }

        public void Update(string value)
        {
            Apply(new BrandUpdated(value));
        }

        public Guid BrandId { get; private set; }
        public Guid LogoDigitalAssetId { get; private set; }
        public DateTime? Deleted { get; private set; }
    }
}
