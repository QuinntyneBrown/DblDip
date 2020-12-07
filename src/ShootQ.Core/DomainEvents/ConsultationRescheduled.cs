using System;

namespace ShootQ.Core.DomainEvents
{
    public record ConsultationRescheduled(DateTime StartDate, DateTime EndDate);

}
