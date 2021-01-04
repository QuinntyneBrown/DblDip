using DblDip.Core.Models;

namespace DblDip.Testing.Builders
{
    public class ExpenseBuilder
    {
        private Expense _expense;

        public static Expense WithDefaults()
        {
            return new Expense();
        }

        public ExpenseBuilder()
        {
            _expense = WithDefaults();
        }

        public Expense Build()
        {
            return _expense;
        }
    }
}
