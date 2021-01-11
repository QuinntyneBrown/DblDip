using DblDip.Core.Data;
using DblDip.Core.Models;
using FluentValidation;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace DblDip.Domain.Features
{
    public class UpdateBrand
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
            private readonly IDblDipDbContext _context;

            public Handler(IDblDipDbContext context) => _context = context;

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {

                var brand = await _context.FindAsync<Brand>(request.Brand.BrandId);

                brand.Update();

                _context.Add(brand);

                await _context.SaveChangesAsync(cancellationToken);

                return new Response()
                {
                    Brand = brand.ToDto()
                };
            }
        }
    }
}
