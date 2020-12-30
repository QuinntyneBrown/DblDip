using BuildingBlocks.Abstractions;
using DblDip.Core.Models;
using FluentValidation;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace DblDip.Domain.Features.Portraits
{
    public class UpdatePortrait
    {
        public class Validator : AbstractValidator<Request>
        {
            public Validator()
            {
                RuleFor(request => request.Portrait).NotNull();
                RuleFor(request => request.Portrait).SetValidator(new PortraitValidator());
            }
        }

        public class Request : IRequest<Response>
        {
            public PortraitDto Portrait { get; init; }
        }

        public class Response
        {
            public PortraitDto Portrait { get; init; }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            private readonly IAppDbContext _context;

            public Handler(IAppDbContext context) => _context = context;

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {

                var portrait = await _context.FindAsync<Portrait>(request.Portrait.PortraitId);

                portrait.Update();

                _context.Store(portrait);

                await _context.SaveChangesAsync(cancellationToken);

                return new Response()
                {
                    Portrait = portrait.ToDto()
                };
            }
        }
    }
}
