using BuildingBlocks.EventStore;
using DblDip.Core.DomainEvents;
using System;

namespace DblDip.Core.Models
{
    public class Board : AggregateRoot
    {
        public Guid BoardId { get; private set; }
        public string Name { get; private set; }
        public BoardState State { get; private set; }
        public DateTime? Deleted { get; private set; }

        protected override void When(dynamic @event) => When(@event);

        public Board(string name)
        {
            Apply(new BoardCreated(Guid.NewGuid(), name));
        }

        private Board()
        {

        }

        public void When(BoardCreated boardCreated)
        {
            BoardId = boardCreated.BoardId;
            Name = boardCreated.Name;
        }

        public void When(BoardUpdated boardUpdated)
        {

        }

        public void When(BoardRemoved boardRemoved)
        {
            Deleted = boardRemoved.Deleted;
        }

        protected override void EnsureValidState()
        {

        }

        public void Update()
        {

        }

        public void Remove(DateTime deleted)
        {
            Apply(new BoardRemoved(deleted));
        }

    }
}
