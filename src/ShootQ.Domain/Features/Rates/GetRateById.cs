using BuildingBlocks.Abstractions;
using ShootQ.Core.Models;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ShootQ.Domain.Features.Rates
{
    public class GetRateById
    {
        public class Request : IRequest<Response> {  
            public Guid RateId { get; set; }        
        }

        public class Response
        {
            public RateDto Rate { get; set; }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            private readonly IAppDbContext _context;

            public Handler(IAppDbContext context) => _context = context;

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken) {

                var rate = await _context.FindAsync<Rate>(request.RateId);

                return new Response() { 
                    Rate = rate.ToDto()
                };
            }
        }
    }
}
