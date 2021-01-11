using BuildingBlocks.EventStore;
using System;

namespace DblDip.Core.DomainEvents
{
    public record QuestionnaireCreated (Guid QuestionnaireId): Event;
    public record QuestionnaireUpdated: Event;
    public record QuestionnaireRemoved (DateTime Deleted): Event;
}
