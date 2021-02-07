using BuildingBlocks.Core;
using DblDip.Core.Data;
using DblDip.Core.Models;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DblDip.Domain.Features
{
    public class GetBrandById
    {
        public class Request : IRequest<Response>
        {
            public Guid BrandId { get; init; }
        }

        public class Response: ResponseBase
        {
            public BrandDto Brand { get; init; }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            private readonly IDblDipDbContext _context;

            public Handler(IDblDipDbContext context) => _context = context;

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {

                var brand = await _context.FindAsync<Brand>(request.BrandId);

                return new Response()
                {
                    Brand = brand.ToDto()
                };
            }
        }
    }
}
