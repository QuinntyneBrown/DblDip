using BuildingBlocks.Abstractions;
using System;

namespace DblDip.Core.Models
{
    public class Point : AggregateRoot
    {
        protected override void When(dynamic @event) => When(@event);

        protected override void EnsureValidState()
        {

        }

        public Guid PointId { get; private set; }
        public DateTime? Deleted { get; private set; }
    }
}
