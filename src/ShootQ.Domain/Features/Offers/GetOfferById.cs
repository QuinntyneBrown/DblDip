using BuildingBlocks.Abstractions;
using ShootQ.Core.Models;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ShootQ.Domain.Features.Offers
{
    public class GetOfferById
    {
        public class Request : IRequest<Response> {  
            public Guid OfferId { get; set; }        
        }

        public class Response
        {
            public OfferDto Offer { get; set; }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            private readonly IAppDbContext _context;

            public Handler(IAppDbContext context) => _context = context;

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken) {

                var offer = await _context.FindAsync<Offer>(request.OfferId);

                return new Response() { 
                    Offer = offer.ToDto()
                };
            }
        }
    }
}
