using BuildingBlocks.Abstractions;
using DblDip.Core.DomainEvents;
using System;

namespace DblDip.Core.Models
{
    public class Story : AggregateRoot
    {
        public Story()
        {
            Apply(new StoryCreated(Guid.NewGuid()));
        }
        protected override void When(dynamic @event) => When(@event);

        public void When(StoryCreated storyCreated)
        {
            StoryId = storyCreated.StoryId;
        }

        public void When(StoryUpdated storyUpdated)
        {

        }

        public void When(StoryRemoved storyRemoved)
        {
            Deleted = storyRemoved.Deleted;
        }

        protected override void EnsureValidState()
        {

        }

        public void Update()
        {

        }

        public void Remove(DateTime deleted)
        {
            Apply(new StoryRemoved(deleted));
        }

        public Guid StoryId { get; private set; }
        public DateTime? Deleted { get; private set; }
    }
}
