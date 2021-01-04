using DblDip.Domain.Features.Feedbacks;

namespace DblDip.Testing.Builders
{
    public class FeedbackDtoBuilder
    {
        private FeedbackDto _feedbackDto;

        public static FeedbackDto WithDefaults()
        {
            return new FeedbackDto(default, default, default);
        }

        public FeedbackDtoBuilder()
        {
            _feedbackDto = WithDefaults();
        }

        public FeedbackDto Build()
        {
            return _feedbackDto;
        }
    }
}
