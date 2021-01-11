using DblDip.Core.Data;
using DblDip.Core.Models;
using FluentValidation;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace DblDip.Domain.Features
{
    public class CreateRate
    {
        public class Validator : AbstractValidator<Request>
        {
            public Validator()
            {
                RuleFor(request => request.Rate).NotNull();
                RuleFor(request => request.Rate).SetValidator(new RateValidator());
            }
        }

        public class Request : IRequest<Response>
        {
            public RateDto Rate { get; init; }
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

                var rate = new Rate(default, default, default);

                _context.Add(rate);

                await _context.SaveChangesAsync(cancellationToken);

                return new Response()
                {
                    Rate = rate.ToDto()
                };
            }
        }
    }
}
