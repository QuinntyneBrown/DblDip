using BuildingBlocks.Core;
using BuildingBlocks.EventStore;
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

        public class Response: ResponseBase
        {
            public FamilyPortraitDto FamilyPortrait { get; init; }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            private readonly IEventStore _store;

            public Handler(IEventStore store) => _store = store;

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {

                var familyPortrait = await _store.FindAsync<FamilyPortrait>(request.FamilyPortrait.FamilyPortraitId);

                familyPortrait.Update();

                _store.Add(familyPortrait);

                await _store.SaveChangesAsync(cancellationToken);

                return new Response()
                {
                    FamilyPortrait = familyPortrait.ToDto()
                };
            }
        }
    }
}
