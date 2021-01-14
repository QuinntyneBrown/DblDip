using BuildingBlocks.EventStore;
using DblDip.Core.Models;
using FluentValidation;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace DblDip.Domain.Features
{
    public class UpdateRate
    {
        public class Validator : AbstractValidator<Request>
        {
            public Validator()
            {
                RuleFor(request => request.Rate).NotNull();
                RuleFor(request => request.Rate).SetValidator(new RateValidator());
            }
        }

        public class Request : IRequest<Response>
        {
            public RateDto Rate { get; init; }
        }

        public class Response
        {
            public RateDto Rate { get; init; }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            private readonly IEventStore _store;

            public Handler(IEventStore store) => _store = store;

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {

                var rate = await _store.FindAsync<Rate>(request.Rate.RateId);

                rate.Update();

                _store.Add(rate);

                await _store.SaveChangesAsync(cancellationToken);

                return new Response()
                {
                    Rate = rate.ToDto()
                };
            }
        }
    }
}
