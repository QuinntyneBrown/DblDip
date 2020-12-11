using System;

namespace DblDip.Domain.Features.Testimonials
{
    public class TestimonialDto
    {
        public Guid TestimonialId { get; set; }
        public Guid PhotographerId { get; set; }
        public Guid ClientId { get; set; }
        public string Description { get; set; }
    }
}
