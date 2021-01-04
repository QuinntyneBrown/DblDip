using BuildingBlocks.Abstractions;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DblDip.Domain.Features
{
    public class GetEquipment
    {
        public class Request : IRequest<Response> { }

        public class Response
        {
            public List<EquipmentDto> Equipment { get; init; }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            private readonly IAppDbContext _context;

            public Handler(IAppDbContext context) => _context = context;

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {
                return new Response()
                {
                    Equipment = _context.Set<DblDip.Core.Models.Equipment>().Select(x => x.ToDto()).ToList()
                };
            }
        }
    }
}
