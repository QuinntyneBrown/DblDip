using ShootQ.Core.Models;

namespace ShootQ.Testing.Builders.Core.Models
{
    public class TestimonialBuilder
    {
        private Testimonial _testimonial;

        public static Testimonial WithDefaults()
        {
            return new Testimonial(default);
        }

        public TestimonialBuilder()
        {
            _testimonial = WithDefaults();
        }

        public Testimonial Build()
        {
            return _testimonial;
        }
    }
}
