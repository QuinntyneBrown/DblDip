using ShootQ.Core.Models;
using ShootQ.Core.ValueObjects;
using System;
using System.Collections.Generic;

namespace ShootQ.Core.DomainEvents
{
    public record SurveyCreated (Guid SurveyId, string Name);
    public record SurveyQuestionAdded(string Value);
    public record SurveyResultAdded(Guid SurveyResultId, Email ClientEmail, IEnumerable<Answer> Answers);
}
