using BuildingBlocks.EventStore;
using DblDip.Core.Models;
using FluentValidation;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace DblDip.Domain.Features
{
    public class CreateShotList
    {
        public class Validator : AbstractValidator<Request>
        {
            public Validator()
            {
                RuleFor(request => request.ShotList).NotNull();
                RuleFor(request => request.ShotList).SetValidator(new ShotListValidator());
            }
        }

        public class Request : IRequest<Response>
        {
            public ShotListDto ShotList { get; init; }
        }

        public class Response
        {
            public ShotListDto ShotList { get; init; }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            private readonly IEventStore _store;

            public Handler(IEventStore store) => _store = store;

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {

                var shotList = new ShotList();

                _store.Add(shotList);

                await _store.SaveChangesAsync(cancellationToken);

                return new Response()
                {
                    ShotList = shotList.ToDto()
                };
            }
        }
    }
}
