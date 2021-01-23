using BuildingBlocks.EventStore;
using DblDip.Core.DomainEvents;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DblDip.Core.Models
{
    public class Epic : AggregateRoot
    {
        public Guid EpicId { get; private set; }
        public Guid AuthorId { get; private set; }
        public string Description { get; private set; }
        private IEnumerable<TicketReference> _ticketReferences;
        public List<TicketReference> TicketReferences => _ticketReferences.ToList();
        private List<StoryReference> _storyReferences;
        public List<StoryReference> StoryReferences => _storyReferences.ToList();
        public DateTime? Deleted { get; private set; }
        public Epic()
        {
            Apply(new EpicCreated(Guid.NewGuid()));
        }
        protected override void When(dynamic @event) => When(@event);

        public void When(EpicCreated epicCreated)
        {
            EpicId = epicCreated.EpicId;
            _ticketReferences = new List<TicketReference>();
            _storyReferences = new List<StoryReference>();
        }

        public void When(EpicUpdated epicUpdated)
        {

        }

        public void When(EpicRemoved epicRemoved)
        {
            Deleted = epicRemoved.Deleted;
        }

        protected override void EnsureValidState()
        {

        }

        public void Update()
        {

        }

        public void Remove(DateTime deleted)
        {
            Apply(new EpicRemoved(deleted));
        }


    }
}
