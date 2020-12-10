using BuildingBlocks.Abstractions;
using ShootQ.Core.Models;
using FluentValidation;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace ShootQ.Domain.Features.Rates
{
    public class UpdateRate
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
            public RateDto Rate { get; set; }
        }

        public class Response
        {
            public RateDto Rate { get; set; }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            private readonly IAppDbContext _context;

            public Handler(IAppDbContext context) => _context = context;

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {

                var rate = await _context.FindAsync<Rate>(request.Rate.RateId);

                //rate.Update();

                _context.Store(rate);

                await _context.SaveChangesAsync(cancellationToken);

                return new Response()
                {
                    Rate = rate.ToDto()
                };
            }
        }
    }
}
