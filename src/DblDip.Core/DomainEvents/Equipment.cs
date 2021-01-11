using BuildingBlocks.EventStore;
using DblDip.Core.ValueObjects;
using System;

namespace DblDip.Core.DomainEvents
{
    public record EquipmentCreated(Guid EquipmentId, string Name, Price Price, string Description): Event;
    public record EquipmentUpdated: Event;
    public record EquipmentRemoved (DateTime Deleted): Event;
}
