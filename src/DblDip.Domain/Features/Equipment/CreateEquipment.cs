using BuildingBlocks.EventStore;
using FluentValidation;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace DblDip.Domain.Features
{
    public class CreateEquipment
    {
        public class Validator : AbstractValidator<Request>
        {
            public Validator()
            {
                RuleFor(request => request.Equipment).NotNull();
                RuleFor(request => request.Equipment).SetValidator(new EquipmentValidator());
            }
        }

        public record Request(EquipmentDto Equipment) : IRequest<Response>;

        public record Response(EquipmentDto Equipment);

        public class Handler : IRequestHandler<Request, Response>
        {
            private readonly IEventStore _store;

            public Handler(IEventStore store) => _store = store;

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {
                var equipment = new DblDip.Core.Models.Equipment(request.Equipment.Name, request.Equipment.Price, request.Equipment.Description);

                _store.Add(equipment);

                await _store.SaveChangesAsync(cancellationToken);

                return new (equipment.ToDto());
            }
        }
    }
}
