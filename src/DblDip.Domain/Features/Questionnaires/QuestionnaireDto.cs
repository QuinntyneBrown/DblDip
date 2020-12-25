using System;

namespace DblDip.Domain.Features.Questionnaires
{
    public class QuestionnaireDto
    {
        public Guid QuestionnaireId { get; init; }
        public DateTime? Deleted { get; init; }
    }
}
