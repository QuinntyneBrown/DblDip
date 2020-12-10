using BuildingBlocks.Abstractions;
using ShootQ.Core.Models;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ShootQ.Domain.Features.Roles
{
    public class GetRoleById
    {
        public class Request : IRequest<Response>
        {
            public Guid RoleId { get; set; }
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

                var role = await _context.FindAsync<Role>(request.RoleId);

                return new Response()
                {
                    Role = role.ToDto()
                };
            }
        }
    }
}