using BuildingBlocks.Abstractions;
using DblDip.Core.Models;
using FluentValidation;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace DblDip.Domain.Features.Testimonials
{
    public class CreateTestimonial
    {
        public class Validator : AbstractValidator<Request>
        {
            public Validator()
            {
                RuleFor(request => request.Testimonial).NotNull();
                RuleFor(request => request.Testimonial).SetValidator(new TestimonialValidator());
            }
        }

        public class Request : IRequest<Response> {  
            public TestimonialDto Testimonial { get; set; }
        }

        public class Response
        {
            public TestimonialDto Testimonial { get; set; }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            private readonly IAppDbContext _context;

            public Handler(IAppDbContext context) => _context = context;

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken) {

                var testimonial = new Testimonial(default);

                _context.Store(testimonial);

                await _context.SaveChangesAsync(cancellationToken);

                return new Response()
                {
                    Testimonial = testimonial.ToDto()
                };
            }
        }
    }
}
