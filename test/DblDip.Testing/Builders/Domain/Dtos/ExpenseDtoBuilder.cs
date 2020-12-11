using DblDip.Core.Models;
using DblDip.Domain.Features.Expenses;

namespace DblDip.Testing.Builders.Domain.Dtos
{
    public class ExpenseDtoBuilder
    {
        private ExpenseDto _expenseDto;

        public static ExpenseDto WithDefaults()
        {
            return new ExpenseDto();
        }

        public ExpenseDtoBuilder()
        {
            _expenseDto = WithDefaults();
        }

        public ExpenseDto Build()
        {
            return _expenseDto;
        }
    }
}
