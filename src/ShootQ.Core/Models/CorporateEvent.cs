using BuildingBlocks.Abstractions;
using ShootQ.Core.ValueObjects;
using System;

namespace ShootQ.Core.Models
{
    public class CorporateEvent: PhotographyJob
    {
        protected override void When(dynamic @event) => When(@event);

        protected override void EnsureValidState()
        {

        }

        public Guid CoporateEventId { get; private set; }

        public override DateRange Scheduled { get; }
    }
}
