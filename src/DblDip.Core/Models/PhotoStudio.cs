using BuildingBlocks.EventStore;
using System;
using DblDip.Core.ValueObjects;
using DblDip.Core.DomainEvents;

namespace DblDip.Core.Models
{
    public class PhotoStudio : AggregateRoot
    {
        public Guid PhotoStudioId { get; private set; }
        public DateTime? Deleted { get; set; }
        public PhotoStudio()
        {
            Apply(new PhotoStudioCreated(Guid.NewGuid()));
        }
        protected override void When(dynamic @event) => When(@event);

        public void When(PhotoStudioCreated photoStudioCreated)
        {
            PhotoStudioId = photoStudioCreated.PhotoStudioId;
        }

        public void When(PhotoStudioUpdated photoStudioUpdated)
        {

        }

        public void When(PhotoStudioRemoved photoStudioRemoved)
        {
            Deleted = photoStudioRemoved.Deleted;
        }

        protected override void EnsureValidState()
        {

        }
        public string Name { get; private set; }
        public Location Location { get; private set; }

        public void Update()
        {
            Apply(new PhotoStudioUpdated());
        }

        public void Remove(DateTime deleted)
        {
            Apply(new PhotoStudioRemoved(deleted));
        }
    }
}
