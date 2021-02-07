using BuildingBlocks.Core;
using BuildingBlocks.EventStore;
using DblDip.Core.Models;
using FluentValidation;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace DblDip.Domain.Features
{
    public class UpdateService
    {
        public class Validator : AbstractValidator<Request>
        {
            public Validator()
            {
                RuleFor(request => request.Service).NotNull();
                RuleFor(request => request.Service).SetValidator(new ServiceValidator());
            }
        }

        public class Request : IRequest<Response>
        {
            public ServiceDto Service { get; init; }
        }

        public class Response: ResponseBase
        {
            public ServiceDto Service { get; init; }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            private readonly IEventStore _store;

            public Handler(IEventStore store) => _store = store;

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {

                var service = await _store.FindAsync<Service>(request.Service.ServiceId);

                service.Update(default);

                _store.Add(service);

                await _store.SaveChangesAsync(cancellationToken);

                return new Response()
                {
                    Service = service.ToDto()
                };
            }
        }
    }
}
