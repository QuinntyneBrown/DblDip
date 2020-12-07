using System;

namespace ShootQ.Core.DomainEvents
{
    public record SurveyCreated (Guid SurveyId, string Name);
}
