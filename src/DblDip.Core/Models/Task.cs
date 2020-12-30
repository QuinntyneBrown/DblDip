using BuildingBlocks.Abstractions;
using DblDip.Core.DomainEvents;
using DblDip.Core.Interfaces;
using DblDip.Core.ValueObjects;
using System;

namespace DblDip.Core.Models
{
    public class Task : AggregateRoot, IScheduledAggregate
    {
        public Task()
        {
            Apply(new TaskCreated(Guid.NewGuid()));
        }
        protected override void When(dynamic @event) => When(@event);

        public void When(TaskCreated taskCreated)
        {
            TaskId = taskCreated.TaskId;
        }

        public void When(TaskUpdated taskUpdated)
        {

        }


        public void When(TaskRemoved taskRemoved)
        {
            Deleted = taskRemoved.Deleted;
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

        public Guid TaskId { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public DateTime? DueDate { get; private set; }
        public DateRange Scheduled { get; private set; }
        public DateTime? Deleted { get; private set; }
    }
}
