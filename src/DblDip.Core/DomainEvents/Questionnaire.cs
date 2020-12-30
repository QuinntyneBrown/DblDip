using System;

namespace DblDip.Core.DomainEvents
{
    public record QuestionnaireCreated (Guid QuestionnaireId);
    public record QuestionnaireUpdated;
    public record QuestionnaireRemoved (DateTime Deleted);
}
