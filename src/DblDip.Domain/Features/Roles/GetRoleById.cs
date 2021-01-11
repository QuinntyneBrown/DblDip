using DblDip.Core.Data;
using DblDip.Core.Models;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DblDip.Domain.Features
{
    public class GetRoleById
    {
        public class Request : IRequest<Response>
        {
            public Guid RoleId { get; init; }
        }

        public class Response
        {
            public RoleDto Role { get; init; }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            private readonly IDblDipDbContext _context;

            public Handler(IDblDipDbContext context) => _context = context;

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
