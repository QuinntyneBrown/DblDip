using DblDip.Core.Data;
using DblDip.Core.Models;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DblDip.Domain.Features
{
    public class GetSystemAdministratorById
    {
        public class Request : IRequest<Response>
        {
            public Guid SystemAdministratorId { get; init; }
        }

        public class Response
        {
            public SystemAdministratorDto SystemAdministrator { get; init; }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            private readonly IDblDipDbContext _context;

            public Handler(IDblDipDbContext context) => _context = context;

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {

                var systemAdministrator = await _context.FindAsync<SystemAdministrator>(request.SystemAdministratorId);

                return new Response()
                {
                    SystemAdministrator = systemAdministrator.ToDto()
                };
            }
        }
    }
}
