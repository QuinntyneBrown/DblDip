using BuildingBlocks.EventStore;
using DblDip.Core.Models;
using FluentValidation;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace DblDip.Domain.Features
{
    public class UpdateReferral
    {
        public class Validator : AbstractValidator<Request>
        {
            public Validator()
            {
                RuleFor(request => request.Referral).NotNull();
                RuleFor(request => request.Referral).SetValidator(new ReferralValidator());
            }
        }

        public class Request : IRequest<Response>
        {
            public ReferralDto Referral { get; init; }
        }

        public class Response
        {
            public ReferralDto Referral { get; init; }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            private readonly IEventStore _store;

            public Handler(IEventStore store) => _store = store;

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {

                var referral = await _store.FindAsync<Referral>(request.Referral.ReferralId);

                referral.Update();

                _store.Add(referral);

                await _store.SaveChangesAsync(cancellationToken);

                return new Response()
                {
                    Referral = referral.ToDto()
                };
            }
        }
    }
}
