using BuildingBlocks.Core;
using BuildingBlocks.EventStore;
using DblDip.Core.Models;
using FluentValidation;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace DblDip.Domain.Features
{
    public class UpdateOrder
    {
        public class Validator : AbstractValidator<Request>
        {
            public Validator()
            {
                RuleFor(request => request.Order).NotNull();
                RuleFor(request => request.Order).SetValidator(new OrderValidator());
            }
        }

        public class Request : IRequest<Response>
        {
            public OrderDto Order { get; init; }
        }

        public class Response: ResponseBase
        {
            public OrderDto Order { get; init; }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            private readonly IEventStore _store;

            public Handler(IEventStore store) => _store = store;

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {

                var order = await _store.FindAsync<Order>(request.Order.OrderId);

                order.Update();

                _store.Add(order);

                await _store.SaveChangesAsync(cancellationToken);

                return new Response()
                {
                    Order = order.ToDto()
                };
            }
        }
    }
}
