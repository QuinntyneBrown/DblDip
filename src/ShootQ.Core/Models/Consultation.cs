using BuildingBlocks.Abstractions;
using ShootQ.Core.DomainEvents;
using ShootQ.Core.ValueObjects;
using System;

namespace ShootQ.Core.Models
{
    public class Consultation: AggregateRoot
    {
        protected override void When(dynamic @event) => When(@event);

        public Consultation(DateRange dateRange, Email customerEmail)
        {
            Apply(new ConsultationCreated(Guid.NewGuid(), customerEmail, dateRange));
        }

        public void When(ConsultationCreated consultationCreated)
        {
            ConsultationId = consultationCreated.ConsultationId;
            DateRange = consultationCreated.DateRange;
            CustomerEmail = consultationCreated.CustomerEmail;
        }

        public void When(ConsultationNoteAdded consultationAdded)
        {
            Note = consultationAdded.Note;
        }

        public void When(ConsultationRemoved consultationRemoved)
        {
            Deleted = consultationRemoved.Deleted;
        }

        public void When(ConsultationRescheduled consultationRescheduled)
        {
            DateRange = DateRange.Create(consultationRescheduled.StartDate, consultationRescheduled.EndDate).Value;
        }

        protected override void EnsureValidState()
        {

        }

        public void AddNote(string value)
        {
            Apply(new ConsultationNoteAdded(value));
        }

        public void Remove(DateTime deleted)
        {
            Apply(new ConsultationRemoved(deleted));
        }

        public void Reschedule(DateTime startDate, DateTime endDate)
        {
            Apply(new ConsultationRescheduled(startDate,endDate));
        }

        public Guid ConsultationId { get; private set; }
        public DateRange DateRange { get; private set; }
        public Email CustomerEmail { get; set; }
        public string Note { get; private set; }
        public DateTime? Deleted { get; set; }
    }
}
