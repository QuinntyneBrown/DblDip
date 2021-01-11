using BuildingBlocks.EventStore;
using DblDip.Core.DomainEvents;
using DblDip.Core.Interfaces;
using DblDip.Core.ValueObjects;
using System;

namespace DblDip.Core.Models
{
    public class Task : AggregateRoot, IScheduledAggregate
    {
        protected Task()
        {

        }

        public Guid TaskId { get; private set; }
        public Guid OwnerId { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public DateTime? DueDate { get; private set; }
        public DateRange Scheduled { get; private set; }
        public DateTime? Deleted { get; private set; }
        public DateTime? Completed { get; set; }
        public Task(Guid ownerId, string description)
        {
            Apply(new TaskCreated(Guid.NewGuid(), ownerId, description));
        }
        protected override void When(dynamic @event) => When(@event);

        public void When(TaskCreated taskCreated)
        {
            TaskId = taskCreated.TaskId;
        }

        public void When(TaskUpdated taskUpdated)
        {
            if (string.IsNullOrEmpty(Description) && string.IsNullOrEmpty(Name))
                throw new Exception();
        }


        public void When(TaskRemoved taskRemoved)
        {
            Deleted = taskRemoved.Deleted;
        }

        public void When(TaskCompleted taskCompleted)
        {
            Completed = taskCompleted.Completed;
        }

        protected override void EnsureValidState()
        {

        }

        public void Update()
        {

        }

        public void Remove(DateTime deleted)
        {
            Apply(new TaskRemoved(deleted));
        }

        public void Complete(DateTime completed)
        {
            Apply(new TaskCompleted(completed));
        }
    }
}
