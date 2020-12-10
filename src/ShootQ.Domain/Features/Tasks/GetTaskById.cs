using BuildingBlocks.Abstractions;
using ShootQ.Core.Models;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ShootQ.Domain.Features.Tasks
{
    public class GetTaskById
    {
        public class Request : IRequest<Response>
        {
            public Guid TaskId { get; set; }
        }

        public class Response
        {
            public TaskDto Task { get; set; }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            private readonly IAppDbContext _context;

            public Handler(IAppDbContext context) => _context = context;

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {

                var task = await _context.FindAsync<ShootQ.Core.Models.Task>(request.TaskId);

                return new Response()
                {
                    Task = task.ToDto()
                };
            }
        }
    }
}
