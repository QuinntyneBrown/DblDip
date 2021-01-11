using DblDip.Core.Data;
using DblDip.Core.Models;
using FluentValidation;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace DblDip.Domain.Features
{
    public class UpdateFamilyPortrait
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
            private readonly IDblDipDbContext _context;

            public Handler(IDblDipDbContext context) => _context = context;

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {

                var familyPortrait = await _context.FindAsync<FamilyPortrait>(request.FamilyPortrait.FamilyPortraitId);

                familyPortrait.Update();

                _context.Add(familyPortrait);

                await _context.SaveChangesAsync(cancellationToken);

                return new Response()
                {
                    FamilyPortrait = familyPortrait.ToDto()
                };
            }
        }
    }
}
