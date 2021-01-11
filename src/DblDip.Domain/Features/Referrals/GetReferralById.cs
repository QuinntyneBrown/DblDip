using DblDip.Core.Data;
using DblDip.Core.Models;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DblDip.Domain.Features
{
    public class GetReferralById
    {
        public class Request : IRequest<Response>
        {
            public Guid ReferralId { get; init; }
        }

        public class Response
        {
            public ReferralDto Referral { get; init; }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            private readonly IDblDipDbContext _context;

            public Handler(IDblDipDbContext context) => _context = context;

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {

                var referral = await _context.FindAsync<Referral>(request.ReferralId);

                return new Response()
                {
                    Referral = referral.ToDto()
                };
            }
        }
    }
}
