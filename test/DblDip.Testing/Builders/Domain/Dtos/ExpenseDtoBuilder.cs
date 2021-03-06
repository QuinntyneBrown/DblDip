using DblDip.Core.Models;
using DblDip.Domain.Features;

namespace DblDip.Testing.Builders
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
