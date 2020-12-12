using BuildingBlocks.Abstractions;
using DblDip.Core.Models;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DblDip.Domain.Features.Brands
{
    public class GetBrandById
    {
        public class Request : IRequest<Response>
        {
            public Guid BrandId { get; set; }
        }

        public class Response
        {
            public BrandDto Brand { get; set; }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            private readonly IAppDbContext _context;

            public Handler(IAppDbContext context) => _context = context;

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
