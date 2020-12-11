using DblDip.Core.ValueObjects;
using System;

namespace DblDip.Core.DomainEvents
{
    public record ConsultationCompleted(DateTime Completed);
    public record ConsultationCreated(Guid ConsultationId, Email ConsultantEmail, Email RecipientEmail, DateRange Scheduled);
    public record ConsultationNoteAdded(string Note);
    public record ConsultationPaid(DateTime Paid);
    public record ConsultationRemoved(DateTime Deleted);
    public record ConsultationRescheduled(DateTime StartDate, DateTime EndDate);
}
