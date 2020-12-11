using BuildingBlocks.Abstractions;
using DblDip.Core.ValueObjects;
using System;

namespace DblDip.Core.Models
{
    public class Portrait : PhotographyProject
    {
        protected override void When(dynamic @event) => When(@event);

        protected override void EnsureValidState()
        {

        }
        public override DateRange Scheduled { get; }
        public Guid PortraitId { get; private set; }
        public DateTime? Deleted { get; private set; }
    }
}
