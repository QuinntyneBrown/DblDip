using BuildingBlocks.Core;
using BuildingBlocks.EventStore;
using DblDip.Core.Models;
using FluentValidation;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace DblDip.Domain.Features
{
    public class CreateSystemAdministrator
    {
        public class Validator : AbstractValidator<Request>
        {
            public Validator()
            {
                RuleFor(request => request.SystemAdministrator).NotNull();
                RuleFor(request => request.SystemAdministrator).SetValidator(new SystemAdministratorValidator());
            }
        }

        public class Request : IRequest<Response>
        {
            public SystemAdministratorDto SystemAdministrator { get; init; }
        }

        public class Response: ResponseBase
        {
            public SystemAdministratorDto SystemAdministrator { get; init; }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            private readonly IEventStore _store;

            public Handler(IEventStore store) => _store = store;

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {

                var systemAdministrator = new SystemAdministrator(default, default);

                _store.Add(systemAdministrator);

                await _store.SaveChangesAsync(cancellationToken);

                return new Response()
                {
                    SystemAdministrator = systemAdministrator.ToDto()
                };
            }
        }
    }
}
