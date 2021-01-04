using DblDip.Domain.Features.Testimonials;

namespace DblDip.Testing.Builders
{
    public class TestimonialDtoBuilder
    {
        private TestimonialDto _testimonialDto;

        public static TestimonialDto WithDefaults()
        {
            return new TestimonialDto();
        }

        public TestimonialDtoBuilder()
        {
            _testimonialDto = WithDefaults();
        }

        public TestimonialDto Build()
        {
            return _testimonialDto;
        }
    }
}
