using BuildingBlocks.EventStore;
using System;

namespace DblDip.Core.DomainEvents
{
    public record PhotographyProjectRemoved (DateTime Deleted): Event;
}
