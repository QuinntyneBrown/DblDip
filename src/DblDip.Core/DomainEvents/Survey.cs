using BuildingBlocks.EventStore;
using DblDip.Core.Models;
using DblDip.Core.ValueObjects;
using System;
using System.Collections.Generic;

namespace DblDip.Core.DomainEvents
{
    public record SurveyCreated(Guid SurveyId, string Name): Event;
    public record SurveyQuestionAdded(string Value): Event;
    public record SurveyResultAdded(Guid SurveyResultId, Email ClientEmail, IEnumerable<Answer> Answers): Event;
    public record SurveyUpdated: Event;
    public record SurveyRemoved (DateTime Deleted): Event;
}
