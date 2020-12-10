using BuildingBlocks.Abstractions;
using ShootQ.Core.Models;
using FluentValidation;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace ShootQ.Domain.Features.Equipment
{
    public class UpdateEquipment
    {
        public class Validator : AbstractValidator<Request>
        {
            public Validator()
            {
                RuleFor(request => request.Equipment).NotNull();
                RuleFor(request => request.Equipment).SetValidator(new EquipmentValidator());
            }
        }

        public class Request : IRequest<Response> {  
            public EquipmentDto Equipment { get; set; }
        }

        public class Response
        {
            public EquipmentDto Equipment { get; set; }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            private readonly IAppDbContext _context;

            public Handler(IAppDbContext context) => _context = context;

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken) {

                var equipment = await _context.FindAsync<ShootQ.Core.Models.Equipment>(request.Equipment.EquipmentId);

                //equipment.Update();

                _context.Store(equipment);

                await _context.SaveChangesAsync(cancellationToken);

                return new Response()
                {
                    Equipment = equipment.ToDto()
                };
            }
        }
    }
}
