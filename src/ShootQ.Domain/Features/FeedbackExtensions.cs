using ShootQ.Core.Models;
using ShootQ.Domain.Features.Feedbacks;

namespace ShootQ.Domain.Features
{
    public static class FeedbackExtensions
    {
        public static FeedbackDto ToDto(this Feedback feedback)
            => new FeedbackDto(feedback.FeedbackId, feedback.RespondentEmail, feedback.Description);
    }
}
