using DblDip.Core.Models;
using DblDip.Domain.Features;

namespace DblDip.Domain.Features
{
    public static class ConsultationExtensions
    {
        public static ConsultationDto ToDto(this Consultation consultation)
            => new ConsultationDto(consultation.ConsultationId, consultation.Scheduled.StartDate, consultation.Scheduled.EndDate, consultation.ConsultantEmail, consultation.RecipientEmail, consultation.Note);
    }
}
