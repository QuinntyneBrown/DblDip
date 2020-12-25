using System;

namespace DblDip.Domain.Features.Expenses
{
    public class ExpenseDto
    {
        public Guid ExpenseId { get; init; }
        public DateTime? Deleted { get; init; }
    }
}
