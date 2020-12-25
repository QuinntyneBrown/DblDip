using BuildingBlocks.Abstractions;
using DblDip.Core.Models;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DblDip.Domain.Features.Testimonials
{
    public class GetTestimonialById
    {
        public class Request : IRequest<Response>
        {
            public Guid TestimonialId { get; init; }
        }

        public class Response
        {
            public TestimonialDto Testimonial { get; init; }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            private readonly IAppDbContext _context;

            public Handler(IAppDbContext context) => _context = context;

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {

                var testimonial = await _context.FindAsync<Testimonial>(request.TestimonialId);

                return new Response()
                {
                    Testimonial = testimonial.ToDto()
                };
            }
        }
    }
}
