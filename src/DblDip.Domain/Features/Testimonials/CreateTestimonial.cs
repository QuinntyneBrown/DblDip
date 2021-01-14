using BuildingBlocks.EventStore;
using DblDip.Core.Models;
using FluentValidation;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace DblDip.Domain.Features
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
            private readonly IEventStore _store;

            public Handler(IEventStore store) => _store = store;

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {

                var testimonial = new Testimonial();

                _store.Add(testimonial);

                await _store.SaveChangesAsync(cancellationToken);

                return new Response()
                {
                    Testimonial = testimonial.ToDto()
                };
            }
        }
    }
}
