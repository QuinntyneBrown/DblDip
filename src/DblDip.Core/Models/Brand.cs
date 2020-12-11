using BuildingBlocks.Abstractions;
using System;

namespace DblDip.Core.Models
{
    public class Brand: AggregateRoot
    {
        protected override void When(dynamic @event) => When(@event);

        protected override void EnsureValidState()
        {

        }

        public Guid BrandId { get; private set; }
        public Guid LogoDigitalAssetId { get; private set; }
        public DateTime? Deleted { get; private set; }
    }
}
