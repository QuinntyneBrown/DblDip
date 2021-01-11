using DblDip.Core.Data;
using DblDip.Core.Models;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DblDip.Domain.Features
{
    public class GetReferrals
    {
        public class Request : IRequest<Response> { }

        public class Response
        {
            public List<ReferralDto> Referrals { get; init; }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            private readonly IDblDipDbContext _context;

            public Handler(IDblDipDbContext context) => _context = context;

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {
                return new Response()
                {
                    Referrals = _context.Set<Referral>().Select(x => x.ToDto()).ToList()
                };
            }
        }
    }
}
