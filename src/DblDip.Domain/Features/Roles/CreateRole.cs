using BuildingBlocks.Core;
using BuildingBlocks.EventStore;
using DblDip.Core.Models;
using FluentValidation;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace DblDip.Domain.Features
{
    public class CreateRole
    {
        public class Validator : AbstractValidator<Request>
        {
            public Validator()
            {
                RuleFor(request => request.Role).NotNull();
                RuleFor(request => request.Role).SetValidator(new RoleValidator());
            }
        }

        public class Request : IRequest<Response>
        {
            public RoleDto Role { get; init; }
        }

        public class Response: ResponseBase
        {
            public RoleDto Role { get; init; }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            private readonly IEventStore _store;

            public Handler(IEventStore store) => _store = store;

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {

                var role = new Role(request.Role.Name);

                _store.Add(role);

                await _store.SaveChangesAsync(cancellationToken);

                return new Response()
                {
                    Role = role.ToDto()
                };
            }
        }
    }
}
