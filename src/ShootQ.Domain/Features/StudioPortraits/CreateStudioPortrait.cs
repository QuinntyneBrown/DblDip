using BuildingBlocks.Abstractions;
using ShootQ.Core.Models;
using FluentValidation;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace ShootQ.Domain.Features.StudioPortraits
{
    public class CreateStudioPortrait
    {
        public class Validator : AbstractValidator<Request>
        {
            public Validator()
            {
                RuleFor(request => request.StudioPortrait).NotNull();
                RuleFor(request => request.StudioPortrait).SetValidator(new StudioPortraitValidator());
            }
        }

        public class Request : IRequest<Response>
        {
            public StudioPortraitDto StudioPortrait { get; set; }
        }

        public class Response
        {
            public StudioPortraitDto StudioPortrait { get; set; }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            private readonly IAppDbContext _context;

            public Handler(IAppDbContext context) => _context = context;

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {

                var studioPortrait = new StudioPortrait();

                _context.Store(studioPortrait);

                await _context.SaveChangesAsync(cancellationToken);

                return new Response()
                {
                    StudioPortrait = studioPortrait.ToDto()
                };
            }
        }
    }
}
