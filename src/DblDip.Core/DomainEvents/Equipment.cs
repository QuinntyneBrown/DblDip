using DblDip.Core.ValueObjects;
using System;

namespace DblDip.Core.DomainEvents
{
    public record EquipmentCreated(Guid EquipmentId, string Name, Price Price, string Description);
}
