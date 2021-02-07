using BuildingBlocks.Core;
using BuildingBlocks.EventStore;
using DblDip.Core.Models;
using FluentValidation;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace DblDip.Domain.Features
{
    public class CreateSystemLocation
    {
        public class Validator : AbstractValidator<Request>
        {
            public Validator()
            {
                RuleFor(request => request.SystemLocation).NotNull();
                RuleFor(request => request.SystemLocation).SetValidator(new SystemLocationValidator());
            }
        }

        public class Request : IRequest<Response>
        {
            public SystemLocationDto SystemLocation { get; init; }
        }

        public class Response: ResponseBase
        {
            public SystemLocationDto SystemLocation { get; init; }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            private readonly IEventStore _store;

            public Handler(IEventStore store) => _store = store;

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {

                var systemLocation = new SystemLocation(request.SystemLocation.Name, request.SystemLocation.Location);

                _store.Add(systemLocation);

                await _store.SaveChangesAsync(cancellationToken);

                return new Response()
                {
                    SystemLocation = systemLocation.ToDto()
                };
            }
        }
    }
}
