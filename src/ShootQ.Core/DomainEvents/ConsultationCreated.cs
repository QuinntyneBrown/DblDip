using ShootQ.Core.ValueObjects;
using System;

namespace ShootQ.Core.DomainEvents
{
    public record ConsultationCreated(Guid ConsultationId, Email CustomerEmail, DateRange DateRange);
}
