using BuildingBlocks.Abstractions;
using System;

namespace DblDip.Core.Models
{
    public class Expense : AggregateRoot
    {
        protected override void When(dynamic @event) => When(@event);

        protected override void EnsureValidState()
        {

        }

        public Guid ExpenseId { get; private set; }
        public string Description { get; set; }
        public ExpenseCategory Category { get; private set; }
        public Guid ProjectId { get; private set; }
        public DateTime? Deleted { get; private set; }
    }

    public enum ExpenseCategory
    {
        Parking,
        EquipmentRental
    }
}
