using BuildingBlocks.EventStore;
using DblDip.Core.DomainEvents;
using System;

namespace DblDip.Core.Models
{
    public class Message : AggregateRoot
    {
        public Message()
        {
            Apply(new MessageCreated(Guid.NewGuid()));
        }
        protected override void When(dynamic @event) => When(@event);

        public void When(MessageRemoved messageRemoved)
        {
            Deleted = messageRemoved.Deleted;
        }

        public void When(MessageCreated messageCreated)
        {
            MessageId = messageCreated.MessageId;
        }

        public void When(MessageUpdated messageUpdated)
        {

        }

        protected override void EnsureValidState()
        {

        }

        public void Remove(DateTime deleted)
        {
            Apply(new MessageRemoved(deleted));
        }


        public void Update()
        {
            Apply(new MessageUpdated());
        }

        public Guid MessageId { get; private set; }
        public Guid ConversationId { get; set; }
        public Guid ToProfileId { get; set; }
        public Guid FromProfileId { get; set; }
        public string Subject { get; set; }
        public string Content { get; set; }
        public bool Read { get; set; }
        public DateTime? Deleted { get; private set; }
    }
}
