using BuildingBlocks.Abstractions;
using ShootQ.Core.Models;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ShootQ.Domain.Features.SystemAdministrators
{
    public class GetSystemAdministratorById
    {
        public class Request : IRequest<Response> {  
            public Guid SystemAdministratorId { get; set; }        
        }

        public class Response
        {
            public SystemAdministratorDto SystemAdministrator { get; set; }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            private readonly IAppDbContext _context;

            public Handler(IAppDbContext context) => _context = context;

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken) {

                var systemAdministrator = await _context.FindAsync<SystemAdministrator>(request.SystemAdministratorId);

                return new Response() { 
                    SystemAdministrator = systemAdministrator.ToDto()
                };
            }
        }
    }
}
