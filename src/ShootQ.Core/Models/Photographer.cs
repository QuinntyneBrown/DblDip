using BuildingBlocks.Abstractions;
using System;

namespace ShootQ.Core.Models
{
    public class Photographer: AggregateRoot
    {
        protected override void When(dynamic @event) => When(@event);

        protected override void EnsureValidState()
        {

        }

        public Guid PhotographerId { get; private set; }
    }
}
