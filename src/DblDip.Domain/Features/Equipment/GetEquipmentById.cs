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
    public class GetEquipmentById
    {
        public class Request : IRequest<Response>
        {
            public Guid EquipmentId { get; init; }
        }

        public class Response: ResponseBase
        {
            public EquipmentDto Equipment { get; init; }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            private readonly IDblDipDbContext _context;

            public Handler(IDblDipDbContext context) => _context = context;

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {

                var equipment = await _context.FindAsync<DblDip.Core.Models.Equipment>(request.EquipmentId);

                return new Response()
                {
                    Equipment = equipment.ToDto()
                };
            }
        }
    }
}
