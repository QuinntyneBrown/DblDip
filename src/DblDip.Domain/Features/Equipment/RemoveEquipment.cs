using BuildingBlocks.Abstractions;
using DblDip.Core.Models;
using FluentValidation;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using System;

namespace DblDip.Domain.Features.Equipment
{
    public class RemoveEquipment
    {
        public class Validator : AbstractValidator<Request>
        {
            public Validator()
            {

            }
        }

        public class Request : IRequest<Unit>
        {
            public Guid EquipmentId { get; init; }
        }

        public class Response
        {
            public EquipmentDto Equipment { get; init; }
        }

        public class Handler : IRequestHandler<Request, Unit>
        {
            private readonly IAppDbContext _context;

            public Handler(IAppDbContext context) => _context = context;

            public async Task<Unit> Handle(Request request, CancellationToken cancellationToken)
            {

                var equipment = await _context.FindAsync<DblDip.Core.Models.Equipment>(request.EquipmentId);

                //equipment.Remove();

                _context.Store(equipment);

                await _context.SaveChangesAsync(cancellationToken);

                return new ()
                {

                };
            }
        }
    }
}
