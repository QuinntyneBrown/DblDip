using BuildingBlocks.Abstractions;
using DblDip.Core.Models;
using FluentValidation;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace DblDip.Domain.Features.Discounts
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
            private readonly IAppDbContext _context;

            public Handler(IAppDbContext context) => _context = context;

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken) {

                var discount = await _context.FindAsync<Discount>(request.Discount.DiscountId);

                discount.Update();

                _context.Store(discount);

                await _context.SaveChangesAsync(cancellationToken);

                return new Response()
                {
                    Discount = discount.ToDto()
                };
            }
        }
    }
}
