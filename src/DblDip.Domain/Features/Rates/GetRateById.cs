using DblDip.Core.Data;
using DblDip.Core.Models;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DblDip.Domain.Features
{
    public class GetRateById
    {
        public class Request : IRequest<Response>
        {
            public Guid RateId { get; init; }
        }

        public class Response
        {
            public RateDto Rate { get; init; }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            private readonly IDblDipDbContext _context;

            public Handler(IDblDipDbContext context) => _context = context;

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {

                var rate = await _context.FindAsync<Rate>(request.RateId);

                return new Response()
                {
                    Rate = rate.ToDto()
                };
            }
        }
    }
}
