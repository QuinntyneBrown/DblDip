using BuildingBlocks.EventStore;
using BuildingBlocks.EventStore;
using DblDip.Core.Models;
using FluentValidation;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using System;

namespace DblDip.Domain.Features
{
    public class RemoveTestimonial
    {
        public class Validator : AbstractValidator<Request>
        {
            public Validator()
            {

            }
        }

        public class Request : IRequest<Unit>
        {
            public Guid TestimonialId { get; init; }
        }

        public class Response
        {
            public TestimonialDto Testimonial { get; init; }
        }

        public class Handler : IRequestHandler<Request, Unit>
        {
            private readonly IEventStore _store;
            private readonly IDateTime _dateTime;

            public Handler(IEventStore store, IDateTime dateTime)
            {
                _store = store;
                _dateTime = dateTime;
            }

            public async Task<Unit> Handle(Request request, CancellationToken cancellationToken)
            {

                var testimonial = await _store.FindAsync<Testimonial>(request.TestimonialId);

                testimonial.Remove(_dateTime.UtcNow);

                _store.Add(testimonial);

                await _store.SaveChangesAsync(cancellationToken);

                return new(); ;
            }
        }
    }
}
