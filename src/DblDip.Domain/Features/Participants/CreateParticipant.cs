using BuildingBlocks.Core;
using BuildingBlocks.EventStore;
using DblDip.Core.Models;
using FluentValidation;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace DblDip.Domain.Features
{
    public class CreateParticipant
    {
        public class Validator : AbstractValidator<Request>
        {
            public Validator()
            {
                RuleFor(request => request.Participant).NotNull();
                RuleFor(request => request.Participant).SetValidator(new ParticipantValidator());
            }
        }

        public class Request : IRequest<Response>
        {
            public ParticipantDto Participant { get; init; }
        }

        public class Response: ResponseBase
        {
            public ParticipantDto Participant { get; init; }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            private readonly IEventStore _store;

            public Handler(IEventStore store) => _store = store;

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {

                var participant = new Participant();

                _store.Add(participant);

                await _store.SaveChangesAsync(cancellationToken);

                return new Response()
                {
                    Participant = participant.ToDto()
                };
            }
        }
    }
}
