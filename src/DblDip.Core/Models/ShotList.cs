using BuildingBlocks.Abstractions;
using DblDip.Core.DomainEvents;
using System;
using System.Collections.Generic;

namespace DblDip.Core.Models
{
    public class ShotList : AggregateRoot
    {
        protected override void When(dynamic @event) => When(@event);

        public ShotList()
        {
            Apply(new ShotListCreated(default));
        }

        public void When(ShotAdded shotAdded)
        {

        }

        public void When(ShotRemoved shotRemoved)
        {

        }

        public void When(ShotListCreated shotListCreated)
        {

        }

        public void When(ShotListUpdated shotListUpdated)
        {

        }

        public void When(ShotListRemoved shotListRemoved)
        {

        }

        protected override void EnsureValidState()
        {

        }

        public void Add(string value)
        {
            Apply(new ShotAdded(value));
        }

        public void Remove(string value)
        {
            Apply(new ShotRemoved(value));
        }

        public void Update(string value)
        {
            Apply(new ShotListUpdated(value));
        }

        public Guid ShotListId { get; private set; }
        public string PhotographyProject { get; set; }
        public ICollection<Shot> Shots { get; set; }
        public DateTime? Deleted { get; private set; }

    }

    public record Shot(string Name, string Description);
}
