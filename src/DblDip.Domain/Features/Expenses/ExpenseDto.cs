using System;

namespace DblDip.Domain.Features
{
    public class ExpenseDto
    {
        public Guid ExpenseId { get; init; }
        public DateTime? Deleted { get; init; }
    }
}
