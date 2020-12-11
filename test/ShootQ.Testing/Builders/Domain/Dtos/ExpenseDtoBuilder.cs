using ShootQ.Core.Models;
using ShootQ.Domain.Features.Expenses;

namespace ShootQ.Testing.Builders.Domain.Dtos
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
