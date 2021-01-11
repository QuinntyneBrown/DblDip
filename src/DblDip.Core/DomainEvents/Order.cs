using BuildingBlocks.EventStore;
using System;

namespace DblDip.Core.DomainEvents
{
    public record OrderPaid(): Event;
    public record OrderCheckedOut(): Event;
    public record OrderRemoved (DateTime Deleted): Event;
    public record OrderUpdated: Event;
}
