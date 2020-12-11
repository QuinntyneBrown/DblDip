using DblDip.Core.ValueObjects;
using System;

namespace DblDip.Core.DomainEvents
{
    public record FeedbackCreated(Guid FeedbackId, Email RespondentEmail, string Description);
}
