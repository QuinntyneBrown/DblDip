using ShootQ.Core.ValueObjects;
using System;

namespace ShootQ.Domain.Features.Feedbacks
{
    public class FeedbackDto
    {
        public Guid FeedbackId { get; set; }
        public Email ClientEmail { get; set; }
        public string Description { get; set; }
    }
}
