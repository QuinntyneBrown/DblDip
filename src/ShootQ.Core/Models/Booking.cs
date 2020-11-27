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
        public Guid CustomerId { get; set; }
        public string Name { get; set; }
        public Guid LocationId { get; set; }
        public DateTime Date { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public ICollection<BookingDetail> BookingDetails { get; set; }

        public record BookingDetail
        {

        }
    }
}
