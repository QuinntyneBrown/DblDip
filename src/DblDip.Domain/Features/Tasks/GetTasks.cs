using BuildingBlocks.Abstractions;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DblDip.Domain.Features
{
    public class GetTasks
    {
        public class Request : IRequest<Response> { }

        public class Response
        {
            public List<TaskDto> Tasks { get; init; }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            private readonly IAppDbContext _context;

            public Handler(IAppDbContext context) => _context = context;

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {
                return new Response()
                {
                    Tasks = _context.Set<DblDip.Core.Models.Task>().Select(x => x.ToDto()).ToList()
                };
            }
        }
    }
}
