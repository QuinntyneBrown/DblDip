using BuildingBlocks.EventStore;
using System;

namespace DblDip.Core.DomainEvents
{
    public record ExpenseCreated (Guid ExpenseId): Event;
    public record ExpenseUpdated: Event;
    public record ExpenseRemoved (DateTime Deleted): Event;
}
