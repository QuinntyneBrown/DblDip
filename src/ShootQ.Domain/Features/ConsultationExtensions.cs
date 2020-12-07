using ShootQ.Core.Models;
using ShootQ.Domain.Features.Consultations;

namespace ShootQ.Domain.Features
{
    public static class ConsultationExtensions
    {
        public static ConsultationDto ToDto(this Consultation consultation)
            => new ConsultationDto(consultation.ConsultationId, consultation.DateRange.StartDate, consultation.DateRange.EndDate, consultation.ClientEmail, consultation.Note);
    }
}
