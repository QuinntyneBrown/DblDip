using BuildingBlocks.Core;
using DblDip.Core.Data;
using DblDip.Core.Models;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DblDip.Domain.Features
{
    public class GetProjectManagerById
    {
        public class Request : IRequest<Response>
        {
            public Guid ProjectManagerId { get; init; }
        }

        public class Response: ResponseBase
        {
            public ProjectManagerDto ProjectManager { get; init; }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            private readonly IDblDipDbContext _context;

            public Handler(IDblDipDbContext context) => _context = context;

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {

                var projectManager = await _context.FindAsync<ProjectManager>(request.ProjectManagerId);

                return new Response()
                {
                    ProjectManager = projectManager.ToDto()
                };
            }
        }
    }
}
