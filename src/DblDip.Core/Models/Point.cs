using BuildingBlocks.EventStore;
using DblDip.Core.DomainEvents;
using System;

namespace DblDip.Core.Models
{
    public class Point : AggregateRoot
    {
        public Point()
        {
            Apply(new PointCreated(Guid.NewGuid()));
        }
        protected override void When(dynamic @event) => When(@event);

        public void When(PointCreated pointCreated)
        {
            PointId = pointCreated.PointId;
        }

        public void When(PointUpdated pointUpdated)
        {

        }

        public void When(PointRemoved pointRemoved)
        {
            Deleted = pointRemoved.Deleted;
        }

        protected override void EnsureValidState()
        {

        }

        public void Update()
        {
            Apply(new PointUpdated());
        }

        public void Remove(DateTime deleted)
        {
            Apply(new PointRemoved(deleted));
        }

        public Guid PointId { get; private set; }
        public DateTime? Deleted { get; private set; }
    }
}
