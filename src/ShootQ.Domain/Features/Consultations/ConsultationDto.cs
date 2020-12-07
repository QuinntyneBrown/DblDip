using ShootQ.Core.ValueObjects;
using System;

namespace ShootQ.Domain.Features.Consultations
{
    public record ConsultationDto(Guid ConsultationId, DateTime StartDate, DateTime EndDate, Email CustomerEmail, string Note);
}
