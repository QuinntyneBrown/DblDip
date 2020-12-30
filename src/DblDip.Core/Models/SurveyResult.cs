using DblDip.Core.ValueObjects;
using System;
using System.Collections.Generic;

namespace DblDip.Core.Models
{
    public record SurveyResult(Guid SurveyResultId, Email RespondentEmail, IEnumerable<Answer> Answers);
}
