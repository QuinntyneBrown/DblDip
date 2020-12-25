using BuildingBlocks.Abstractions;
using DblDip.Core.Models;
using FluentValidation;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace DblDip.Domain.Features.FamilyPortraits
{
    public class CreateFamilyPortrait
    {
        public class Validator : AbstractValidator<Request>
        {
            public Validator()
            {
                RuleFor(request => request.FamilyPortrait).NotNull();
                RuleFor(request => request.FamilyPortrait).SetValidator(new FamilyPortraitValidator());
            }
        }

        public class Request : IRequest<Response>
        {
            public FamilyPortraitDto FamilyPortrait { get; init; }
        }

        public class Response
        {
            public FamilyPortraitDto FamilyPortrait { get; init; }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            private readonly IAppDbContext _context;

            public Handler(IAppDbContext context) => _context = context;

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {

                var familyPortrait = new FamilyPortrait();

                _context.Store(familyPortrait);

                await _context.SaveChangesAsync(cancellationToken);

                return new Response()
                {
                    FamilyPortrait = familyPortrait.ToDto()
                };
            }
        }
    }
}
