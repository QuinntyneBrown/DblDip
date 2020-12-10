using ShootQ.Core.Models;
using ShootQ.Domain.Features.Consultations;

namespace ShootQ.Domain.Features
{
    public static class ConsultationExtensions
    {
        public static ConsultationDto ToDto(this Consultation consultation)
            => new ConsultationDto(consultation.ConsultationId, consultation.Scheduled.StartDate, consultation.Scheduled.EndDate, consultation.RecipientEmail, consultation.Note);
    }
}
