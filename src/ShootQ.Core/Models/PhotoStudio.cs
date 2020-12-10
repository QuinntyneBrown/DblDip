using BuildingBlocks.Abstractions;
using System;
using ShootQ.Core.ValueObjects;

namespace ShootQ.Core.Models
{
    public class PhotoStudio: AggregateRoot
    {
        protected override void When(dynamic @event) => When(@event);

        protected override void EnsureValidState()
        {

        }

        public Location Location { get; set; }
        public Guid PhotoStudioId { get; private set; }
    }
}
