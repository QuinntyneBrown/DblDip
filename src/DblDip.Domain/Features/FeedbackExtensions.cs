using DblDip.Core.Models;
using DblDip.Domain.Features.Feedbacks;

namespace DblDip.Domain.Features
{
    public static class FeedbackExtensions
    {
        public static FeedbackDto ToDto(this Feedback feedback)
            => new FeedbackDto(feedback.FeedbackId, feedback.RespondentEmail, feedback.Description);
    }
}
