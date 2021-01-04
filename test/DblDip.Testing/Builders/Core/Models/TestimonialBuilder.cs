using DblDip.Core.Models;

namespace DblDip.Testing.Builders
{
    public class TestimonialBuilder
    {
        private Testimonial _testimonial;

        public static Testimonial WithDefaults()
        {
            return new Testimonial();
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
