using BuildingBlocks.Abstractions;
using System;

namespace DblDip.Core.Models
{
    public class Participant: AggregateRoot
    {
        protected override void When(dynamic @event) => When(@event);

        protected override void EnsureValidState()
        {

        }

        public Guid ParticipantId { get; private set; }
        public DateTime? Deleted { get; private set; }
    }
}
