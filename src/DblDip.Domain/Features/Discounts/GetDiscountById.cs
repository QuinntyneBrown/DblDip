using BuildingBlocks.Abstractions;
using DblDip.Core.Models;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DblDip.Domain.Features.Discounts
{
    public class GetDiscountById
    {
        public class Request : IRequest<Response> {  
            public Guid DiscountId { get; set; }        
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

                var discount = await _context.FindAsync<Discount>(request.DiscountId);

                return new Response() { 
                    Discount = discount.ToDto()
                };
            }
        }
    }
}
