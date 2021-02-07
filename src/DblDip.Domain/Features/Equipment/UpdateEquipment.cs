using BuildingBlocks.Core;
using BuildingBlocks.EventStore;
using DblDip.Core.Models;
using FluentValidation;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace DblDip.Domain.Features
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

        public class Request : IRequest<Response>
        {
            public EquipmentDto Equipment { get; init; }
        }

        public class Response: ResponseBase
        {
            public EquipmentDto Equipment { get; init; }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            private readonly IEventStore _store;

            public Handler(IEventStore store) => _store = store;

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {

                var equipment = await _store.FindAsync<DblDip.Core.Models.Equipment>(request.Equipment.EquipmentId);

                equipment.Update();

                _store.Add(equipment);

                await _store.SaveChangesAsync(cancellationToken);

                return new Response()
                {
                    Equipment = equipment.ToDto()
                };
            }
        }
    }
}
