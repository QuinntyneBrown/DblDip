using DblDip.Core.Models;
using DblDip.Domain.Features;

namespace DblDip.Domain.Features
{
    public static class ExpenseExtensions
    {
        public static ExpenseDto ToDto(this Expense expense)
        {
            return new ExpenseDto
            {

            };
        }
    }
}
