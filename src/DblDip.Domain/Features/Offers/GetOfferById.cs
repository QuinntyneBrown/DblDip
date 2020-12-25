using BuildingBlocks.Abstractions;
using DblDip.Core.Models;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DblDip.Domain.Features.Offers
{
    public class GetOfferById
    {
        public class Request : IRequest<Response>
        {
            public Guid OfferId { get; init; }
        }

        public class Response
        {
            public OfferDto Offer { get; init; }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            private readonly IAppDbContext _context;

            public Handler(IAppDbContext context) => _context = context;

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {

                var offer = await _context.FindAsync<Offer>(request.OfferId);

                return new Response()
                {
                    Offer = offer.ToDto()
                };
            }
        }
    }
}
