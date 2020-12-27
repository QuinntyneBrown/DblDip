using BuildingBlocks.Abstractions;
using DblDip.Core.ValueObjects;
using System;

namespace DblDip.Core.Models
{
    public class CorporateEvent : PhotographyProject
    {
        public CorporateEvent()
        {

        }
        protected override void When(dynamic @event) => When(@event);

        protected override void EnsureValidState()
        {

        }

        public Guid CorporateEventId { get; private set; }

        public override DateRange Scheduled { get; }
    }
}
