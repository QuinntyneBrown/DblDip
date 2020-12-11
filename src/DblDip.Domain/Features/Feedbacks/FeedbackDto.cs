using DblDip.Core.ValueObjects;
using System;

namespace DblDip.Domain.Features.Feedbacks
{
    public record FeedbackDto(Guid FeedbackId, Email RespondentEmail, string Description);
}
