using BuildingBlocks.EventStore;
using DblDip.Core.Models;
using FluentValidation;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace DblDip.Domain.Features
{
    public class CreatePoint
    {
        public class Validator : AbstractValidator<Request>
        {
            public Validator()
            {
                RuleFor(request => request.Point).NotNull();
                RuleFor(request => request.Point).SetValidator(new PointValidator());
            }
        }

        public class Request : IRequest<Response>
        {
            public PointDto Point { get; init; }
        }

        public class Response
        {
            public PointDto Point { get; init; }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            private readonly IEventStore _store;

            public Handler(IEventStore store) => _store = store;

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {

                var point = new Point();

                _store.Add(point);

                await _store.SaveChangesAsync(cancellationToken);

                return new Response()
                {
                    Point = point.ToDto()
                };
            }
        }
    }
}
