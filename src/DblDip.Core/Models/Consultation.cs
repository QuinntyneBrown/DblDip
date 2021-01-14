using BuildingBlocks.EventStore;
using DblDip.Core.DomainEvents;
using DblDip.Core.Interfaces;
using DblDip.Core.ValueObjects;
using System;

namespace DblDip.Core.Models
{
    public class Consultation : AggregateRoot, IScheduledAggregate
    {
        public Guid ConsultationId { get; private set; }
        public DateRange Scheduled { get; private set; }
        public Email ConsultantEmail { get; private set; }
        public Email RecipientEmail { get; private set; }
        public string RecipientPhoneNumber { get; private set; }
        public string Note { get; private set; }
        public DateTime? Deleted { get; private set; }
        public DateTime? Completed { get; private set; }
        public DateTime? Paid { get; private set; }
        protected Consultation()
        {

        }
        protected override void When(dynamic @event) => When(@event);

        public Consultation(DateRange scheduled, Email consultantEmail, Email recipientEmail)
        {
            Apply(new ConsultationCreated(Guid.NewGuid(), consultantEmail, recipientEmail, scheduled));
        }

        public void When(ConsultationCreated consultationCreated)
        {
            ConsultationId = consultationCreated.ConsultationId;
            Scheduled = consultationCreated.Scheduled;
            ConsultantEmail = consultationCreated.ConsultantEmail;
            RecipientEmail = consultationCreated.RecipientEmail;
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
            Scheduled = DateRange.Create(consultationRescheduled.StartDate, consultationRescheduled.EndDate).Value;
        }

        public void When(ConsultationCompleted consultationCompleted)
        {
            Completed = consultationCompleted.Completed;
        }

        public void When(ConsultationPaid consultationPaid)
        {
            Paid = consultationPaid.Paid;
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
            Apply(new ConsultationRescheduled(startDate, endDate));
        }

        public void Complete(DateTime completed)
        {
            Apply(new ConsultationCompleted(completed));
        }

        public void Pay(DateTime paid)
        {
            Apply(new ConsultationPaid(paid));
        }
    }
}
