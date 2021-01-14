using BuildingBlocks.EventStore;
using DblDip.Core.Models;
using FluentValidation;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace DblDip.Domain.Features
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

        public record Request(FamilyPortraitDto FamilyPortrait) : IRequest<Response>;

        public record Response(FamilyPortraitDto FamilyPortrait);

        public class Handler : IRequestHandler<Request, Response>
        {
            private readonly IEventStore _store;

            public Handler(IEventStore store) => _store = store;

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {
                var familyPortrait = new FamilyPortrait();

                _store.Add(familyPortrait);

                await _store.SaveChangesAsync(cancellationToken);

                return new(familyPortrait.ToDto());
            }
        }
    }
}
