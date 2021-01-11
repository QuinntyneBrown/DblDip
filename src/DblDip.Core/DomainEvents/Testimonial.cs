using BuildingBlocks.EventStore;
using System;

namespace DblDip.Core.DomainEvents
{
    public record TestimonialCreated(Guid TestimonialId): Event;
    public record TestimonialUpdated: Event;
    public record TestimonialRemoved(DateTime Deleted): Event;
}
