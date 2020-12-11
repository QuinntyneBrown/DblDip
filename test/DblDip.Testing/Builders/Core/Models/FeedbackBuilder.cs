using DblDip.Core.Models;

namespace DblDip.Testing.Builders.Core.Models
{
    public class FeedbackBuilder
    {
        private Feedback _feedback;

        public static Feedback WithDefaults()
        {
            return new Feedback(default,default);
        }

        public FeedbackBuilder()
        {
            _feedback = WithDefaults();
        }

        public Feedback Build()
        {
            return _feedback;
        }
    }
}
