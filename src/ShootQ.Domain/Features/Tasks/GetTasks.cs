using BuildingBlocks.Abstractions;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ShootQ.Domain.Features.Tasks
{
    public class GetTasks
    {
        public class Request : IRequest<Response> { }

        public class Response
        {
            public List<TaskDto> Tasks { get; set; }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            private readonly IAppDbContext _context;

            public Handler(IAppDbContext context) => _context = context;

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {
                return new Response()
                {
                    Tasks = _context.Set<ShootQ.Core.Models.Task>().Select(x => x.ToDto()).ToList()
                };
            }
        }
    }
}
