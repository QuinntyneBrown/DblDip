using DblDip.Core.Data;
using DblDip.Core.Models;
using FluentValidation;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace DblDip.Domain.Features
{
    public class UpdateTestimonial
    {
        public class Validator : AbstractValidator<Request>
        {
            public Validator()
            {
                RuleFor(request => request.Testimonial).NotNull();
                RuleFor(request => request.Testimonial).SetValidator(new TestimonialValidator());
            }
        }

        public class Request : IRequest<Response>
        {
            public TestimonialDto Testimonial { get; init; }
        }

        public class Response
        {
            public TestimonialDto Testimonial { get; init; }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            private readonly IDblDipDbContext _context;

            public Handler(IDblDipDbContext context) => _context = context;

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {

                var testimonial = await _context.FindAsync<Testimonial>(request.Testimonial.TestimonialId);

                testimonial.Update();

                _context.Add(testimonial);

                await _context.SaveChangesAsync(cancellationToken);

                return new Response()
                {
                    Testimonial = testimonial.ToDto()
                };
            }
        }
    }
}
