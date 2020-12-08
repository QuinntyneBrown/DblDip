using ShootQ.Core.ValueObjects;
using System;

namespace ShootQ.Core.DomainEvents
{
    public record FeedbackCreated (Guid FeedbackId, Email ClientEmail, string Description);
}
