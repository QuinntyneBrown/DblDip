using BuildingBlocks.Abstractions;
using System;
using System.Collections.Generic;

namespace ShootQ.Core.Models
{
    public class Booking : AggregateRoot
    {
        protected override void When(dynamic @event) => When(@event);

        protected override void EnsureValidState()
        {

        }

        public Guid BookingId { get; private set; }
        public Guid CustomerId { get; private set; }
        public string Name { get; private set; }
        public Guid LocationId { get; private set; }
        public DateTime Date { get; private set; }
        public DateTime StartTime { get; private set; }
        public DateTime EndTime { get; private set; }
        public ICollection<BookingDetail> BookingDetails { get; private set; }

        public record BookingDetail;
    }
}
