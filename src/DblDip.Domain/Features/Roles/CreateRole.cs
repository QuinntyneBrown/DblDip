using BuildingBlocks.Abstractions;
using DblDip.Core.Models;
using FluentValidation;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace DblDip.Domain.Features.Roles
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
            public RoleDto Role { get; set; }
        }

        public class Response
        {
            public RoleDto Role { get; set; }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            private readonly IAppDbContext _context;

            public Handler(IAppDbContext context) => _context = context;

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {

                var role = new Role(request.Role.Name);

                _context.Store(role);

                await _context.SaveChangesAsync(cancellationToken);

                return new Response()
                {
                    Role = role.ToDto()
                };
            }
        }
    }
}
