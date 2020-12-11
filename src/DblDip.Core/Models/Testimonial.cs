using BuildingBlocks.Abstractions;
using DblDip.Core.DomainEvents;
using System;

namespace DblDip.Core.Models
{
    public class Testimonial: AggregateRoot
    {
        protected override void When(dynamic @event) => When(@event);

        public Testimonial(string value)
        {
            Apply(new TestimonialCreated(value));
        }
        public void When(TestimonialCreated testimonialCreated)
        {

        }

        public void When(TestimonialRemoved testimonialRemoved)
        {

        }

        public void When(TestimonialUpdated testimonialUpdated)
        {

        }

        protected override void EnsureValidState()
        {

        }

        public void Remove(string value)
        {
            Apply(new TestimonialRemoved(value));
        }

        public void Update(string value)
        {
            Apply(new TestimonialUpdated(value));
        }

        public Guid TestimonialId { get; private set; }
        public Guid PhotographerId { get; private set; }
        public Guid ClientId { get; private set; }
        public string Description { get; private set; }
        public DateTime? Deleted { get; private set; }
    }
}
