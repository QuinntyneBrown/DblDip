using DblDip.Core.ValueObjects;
using System;

namespace DblDip.Domain.Features
{
    public record FeedbackDto(Guid FeedbackId, Email RespondentEmail, string Description);
}
