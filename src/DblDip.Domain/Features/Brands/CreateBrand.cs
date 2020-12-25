using BuildingBlocks.Abstractions;
using DblDip.Core.Models;
using FluentValidation;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace DblDip.Domain.Features.Brands
{
    public class CreateBrand
    {
        public class Validator : AbstractValidator<Request>
        {
            public Validator()
            {
                RuleFor(request => request.Brand).NotNull();
                RuleFor(request => request.Brand).SetValidator(new BrandValidator());
            }
        }

        public class Request : IRequest<Response>
        {
            public BrandDto Brand { get; init; }
        }

        public class Response
        {
            public BrandDto Brand { get; init; }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            private readonly IAppDbContext _context;

            public Handler(IAppDbContext context) => _context = context;

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {

                var brand = new Brand();

                _context.Store(brand);

                await _context.SaveChangesAsync(cancellationToken);

                return new Response()
                {
                    Brand = brand.ToDto()
                };
            }
        }
    }
}
