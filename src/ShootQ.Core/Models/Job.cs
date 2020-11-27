using BuildingBlocks.Abstractions;
using System;

namespace ShootQ.Core.Models
{
    public class Job : AggregateRoot
    {
        protected override void When(dynamic @event) => When(@event);

        protected override void EnsureValidState()
        {

        }

        public Guid JobId { get; private set; }
    }
}
