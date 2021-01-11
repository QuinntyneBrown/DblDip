using BuildingBlocks.EventStore;
using DblDip.Core.DomainEvents;
using DblDip.Core.Interfaces;
using DblDip.Core.ValueObjects;
using System;

namespace DblDip.Core.Models
{
    public class Meeting : AggregateRoot, IScheduledAggregate
    {
        public Meeting()
        {
            Apply(new MeetingCreated(Guid.NewGuid()));
        }
        protected override void When(dynamic @event) => When(@event);

        public void When(MeetingCreated meetingCreated)
        {
            MeetingId = meetingCreated.MeetingId;
        }

        public void When(MeetingUpdated meetingUpdated)
        {

        }

        public void When(MeetingRemoved meetingRemoved)
        {
            Deleted = meetingRemoved.Deleted;
        }

        protected override void EnsureValidState()
        {

        }

        public void Update()
        {
            Apply(new MeetingUpdated());
        }

        public void Remove(DateTime deleted)
        {
            Apply(new MeetingRemoved(deleted));
        }

        public Guid MeetingId { get; private set; }

        public DateRange Scheduled { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public DateTime? Deleted { get; private set; }
    }
}
