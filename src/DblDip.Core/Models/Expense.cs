using BuildingBlocks.Abstractions;
using DblDip.Core.DomainEvents;
using System;

namespace DblDip.Core.Models
{
    public class Expense : AggregateRoot
    {
        public Expense()
        {
            Apply(new ExpenseCreated(Guid.NewGuid()));
        }
        protected override void When(dynamic @event) => When(@event);

        public void When(ExpenseCreated expenseCreated)
        {
            ExpenseId = expenseCreated.ExpenseId;
        }

        public void When(ExpenseUpdated expenseUpdated)
        {

        }

        public void When(ExpenseRemoved expenseRemoved)
        {
            Deleted = expenseRemoved.Deleted;
        }

        protected override void EnsureValidState()
        {

        }

        public void Update()
        {
            Apply(new ExpenseUpdated());
        }

        public void Remove(DateTime deleted)
        {
            Apply(new ExpenseRemoved(deleted));
        }

        public Guid ExpenseId { get; private set; }
        public string Description { get; private set; }
        public ExpenseCategory Category { get; private set; }
        public Guid ProjectId { get; private set; }
        public DateTime? Deleted { get; private set; }
    }
}
