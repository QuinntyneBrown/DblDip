using ShootQ.Core.Models;
using ShootQ.Core.ValueObjects;
using System;
using System.Collections.Generic;

namespace ShootQ.Core.DomainEvents
{
    public record SurveyResultAdded (Guid SurveyResultId, Email ClientEmail, IEnumerable<Answer> Answers);
}
