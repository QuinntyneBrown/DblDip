using BuildingBlocks.Abstractions;
using DblDip.Core.Models;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DblDip.Domain.Features.FamilyPortraits
{
    public class GetFamilyPortraitById
    {
        public class Request : IRequest<Response>
        {
            public Guid FamilyPortraitId { get; set; }
        }

        public class Response
        {
            public FamilyPortraitDto FamilyPortrait { get; set; }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            private readonly IAppDbContext _context;

            public Handler(IAppDbContext context) => _context = context;

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {

                var familyPortrait = await _context.FindAsync<FamilyPortrait>(request.FamilyPortraitId);

                return new Response()
                {
                    FamilyPortrait = familyPortrait.ToDto()
                };
            }
        }
    }
}
