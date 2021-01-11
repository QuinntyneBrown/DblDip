using BuildingBlocks.EventStore;
using DblDip.Core.DomainEvents;
using DblDip.Core.ValueObjects;
using System;
using System.Collections.Generic;

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
            _taskReferences = new List<Guid>();
        }

        public void When(StoryUpdated storyUpdated)
        {

        }

        public void When(StoryRemoved storyRemoved)
        {
            Deleted = storyRemoved.Deleted;
        }

        public void When(StoryTaskAdded storyTaskAdded)
        {
            _taskReferences.Add(storyTaskAdded.TaskId);
        }

        public void When(StoryTaskRemoved storyTaskRemoved)
        {
            _taskReferences.Remove(storyTaskRemoved.TaskId);
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

        public void AddStoryTask(Guid taskId)
        {
            Apply(new StoryTaskAdded(taskId));
        }

        public void RemoveStoryTask(Guid taskId)
        {
            Apply(new StoryTaskRemoved(taskId));
        }

        public Guid StoryId { get; private set; }
        public StoryPoints StoryPoints { get; private set; }
        public StoryPoints ArchitectureStoryPoints { get; private set; }
        public DateTime? Started { get; private set; }
        public DateTime? Deleted { get; private set; }
        public DateTime? Completed { get; private set; }
        public string Description { get; private set; }
        public IReadOnlyList<Guid> TaskReferences => _taskReferences.AsReadOnly();


        public string Notes { get; private set; }

        private List<Guid> _taskReferences;
    }
}
