using DblDip.Core.ValueObjects;
using System;

namespace DblDip.Domain.Features
{
    public record ConsultationDto(Guid ConsultationId, DateTime StartDate, DateTime EndDate, Email ConsultantEmail, Email RecipientEmail, string Note);
}
