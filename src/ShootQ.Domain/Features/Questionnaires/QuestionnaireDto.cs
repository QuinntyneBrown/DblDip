using System;

namespace ShootQ.Domain.Features.Questionnaires
{
    public class QuestionnaireDto
    {
        public Guid QuestionnaireId { get; private set; }
        public DateTime? Deleted { get; private set; }
    }
}
