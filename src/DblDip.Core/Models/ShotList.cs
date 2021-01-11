using BuildingBlocks.EventStore;
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
            Apply(new ShotListCreated(Guid.NewGuid()));
        }

        public void When(ShotAdded shotAdded)
        {

        }

        public void When(ShotRemoved shotRemoved)
        {

        }

        public void When(ShotListCreated shotListCreated)
        {
            ShotListId = shotListCreated.ShotListId;
        }

        public void When(ShotListUpdated shotListUpdated)
        {

        }

        public void When(ShotListRemoved shotListRemoved)
        {
            Deleted = shotListRemoved.Deleted;
        }

        protected override void EnsureValidState()
        {

        }

        public void Add(string value)
        {
            Apply(new ShotAdded(value));
        }

        public void RemoveShot(DateTime deleted)
        {
            
        }

        public void Remove(DateTime deleted)
        {
            Apply(new ShotListRemoved(deleted));
        }

        public void Update()
        {
            Apply(new ShotListUpdated());
        }

        public Guid ShotListId { get; private set; }
        public string PhotographyProject { get; private set; }
        public ICollection<Shot> Shots { get; private set; }
        public DateTime? Deleted { get; private set; }

    }
}
