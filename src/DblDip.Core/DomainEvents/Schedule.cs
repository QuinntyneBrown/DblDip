using BuildingBlocks.EventStore;
using DblDip.Core.ValueObjects;

namespace DblDip.Core.DomainEvents
{
    public record Rescheduled(DateRange newSchedule): Event;
}
