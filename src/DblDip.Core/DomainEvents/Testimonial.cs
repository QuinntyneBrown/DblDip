using System;

namespace DblDip.Core.DomainEvents
{
    public record TestimonialCreated(Guid TestimonialId);
    public record TestimonialUpdated;
    public record TestimonialRemoved(DateTime Deleted);
}
