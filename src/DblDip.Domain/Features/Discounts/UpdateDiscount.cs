using BuildingBlocks.EventStore;
using DblDip.Core.Models;
using FluentValidation;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace DblDip.Domain.Features
{
    public class UpdateDiscount
    {
        public class Validator : AbstractValidator<Request>
        {
            public Validator()
            {
                RuleFor(request => request.Discount).NotNull();
                RuleFor(request => request.Discount).SetValidator(new DiscountValidator());
            }
        }

        public class Request : IRequest<Response> {  
            public DiscountDto Discount { get; set; }
        }

        public class Response
        {
            public DiscountDto Discount { get; set; }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            private readonly IEventStore _store;

            public Handler(IEventStore store) => _store = store;

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken) {

                var discount = await _store.FindAsync<Discount>(request.Discount.DiscountId);

                discount.Update();

                _store.Add(discount);

                await _store.SaveChangesAsync(cancellationToken);

                return new Response()
                {
                    Discount = discount.ToDto()
                };
            }
        }
    }
}
