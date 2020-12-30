using DblDip.Core.Models;
using DblDip.Core.ValueObjects;
using System;
using System.Collections.Generic;

namespace DblDip.Core.DomainEvents
{
    public record SurveyCreated(Guid SurveyId, string Name);
    public record SurveyQuestionAdded(string Value);
    public record SurveyResultAdded(Guid SurveyResultId, Email ClientEmail, IEnumerable<Answer> Answers);
    public record SurveyUpdated;
    public record SurveyRemoved (DateTime Deleted);
}
