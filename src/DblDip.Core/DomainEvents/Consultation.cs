using BuildingBlocks.EventStore;
using DblDip.Core.ValueObjects;
using System;

namespace DblDip.Core.DomainEvents
{
    public record ConsultationCompleted(DateTime Completed): Event;
    public record ConsultationCreated(Guid ConsultationId, Email ConsultantEmail, Email RecipientEmail, DateRange Scheduled): Event;
    public record ConsultationNoteAdded(string Note) : Event;
    public record ConsultationPaid(DateTime Paid) : Event;
    public record ConsultationRemoved(DateTime Deleted) : Event;
    public record ConsultationRescheduled(DateTime StartDate, DateTime EndDate) : Event;
}
