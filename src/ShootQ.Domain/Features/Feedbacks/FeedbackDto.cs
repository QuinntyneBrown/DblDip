using ShootQ.Core.ValueObjects;
using System;

namespace ShootQ.Domain.Features.Feedbacks
{
    public record FeedbackDto(Guid FeedbackId, Email RespondentEmail, string Description);
}
