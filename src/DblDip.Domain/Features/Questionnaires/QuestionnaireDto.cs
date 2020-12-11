using System;

namespace DblDip.Domain.Features.Questionnaires
{
    public class QuestionnaireDto
    {
        public Guid QuestionnaireId { get; private set; }
        public DateTime? Deleted { get; private set; }
    }
}
