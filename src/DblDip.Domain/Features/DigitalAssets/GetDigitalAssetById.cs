using BuildingBlocks.Abstractions;
using MediatR;
using DblDip.Core.Models;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DblDip.Domain.Features
{
    public class GetDigitalAssetById
    {
        public class Request : IRequest<Response>
        {
            public Guid DigitalAssetId { get; init; }
        }

        public class Response
        {
            public DigitalAssetDto DigitalAsset { get; init; }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            public IAppDbContext _context { get; init; }

            public Handler(IAppDbContext context) => _context = context;

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
                => new Response
                {
                    DigitalAsset = _context.Set<DigitalAsset>().FirstOrDefault(x => x.DigitalAssetId == request.DigitalAssetId).ToDto()
                };
        }
    }
}
