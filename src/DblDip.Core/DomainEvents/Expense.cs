using System;

namespace DblDip.Core.DomainEvents
{
    public record ExpenseCreated (Guid ExpenseId);
    public record ExpenseUpdated;
    public record ExpenseRemoved (DateTime Deleted);
}
