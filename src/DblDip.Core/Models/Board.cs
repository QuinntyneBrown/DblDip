using BuildingBlocks.Abstractions;
using System;
using System.Collections.Generic;

namespace DblDip.Core.Models
{
    public class Board : AggregateRoot
    {
        protected override void When(dynamic @event) => When(@event);

        public Board()
        {

        }
        protected override void EnsureValidState()
        {

        }

        public Guid BoardId { get; private set; }
        public string Name { get; private set; }
        public BoardState State { get; private set; }
        public DateTime? Deleted { get; private set; }

    }

    public record BoardState(string Name, int SortOrder, ICollection<TicketState> TicketStates);
}
