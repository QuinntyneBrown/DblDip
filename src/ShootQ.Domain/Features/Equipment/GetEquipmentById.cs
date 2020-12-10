using BuildingBlocks.Abstractions;
using ShootQ.Core.Models;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ShootQ.Domain.Features.Equipment
{
    public class GetEquipmentById
    {
        public class Request : IRequest<Response>
        {
            public Guid EquipmentId { get; set; }
        }

        public class Response
        {
            public EquipmentDto Equipment { get; set; }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            private readonly IAppDbContext _context;

            public Handler(IAppDbContext context) => _context = context;

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {

                var equipment = await _context.FindAsync<ShootQ.Core.Models.Equipment>(request.EquipmentId);

                return new Response()
                {
                    Equipment = equipment.ToDto()
                };
            }
        }
    }
}
