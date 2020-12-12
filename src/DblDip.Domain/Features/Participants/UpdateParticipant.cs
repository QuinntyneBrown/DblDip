using BuildingBlocks.Abstractions;
using DblDip.Core.Models;
using FluentValidation;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace DblDip.Domain.Features.Participants
{
    public class UpdateParticipant
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
            public ParticipantDto Participant { get; set; }
        }

        public class Response
        {
            public ParticipantDto Participant { get; set; }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            private readonly IAppDbContext _context;

            public Handler(IAppDbContext context) => _context = context;

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {

                var participant = await _context.FindAsync<Participant>(request.Participant.ParticipantId);

                //participant.Update();

                _context.Store(participant);

                await _context.SaveChangesAsync(cancellationToken);

                return new Response()
                {
                    Participant = participant.ToDto()
                };
            }
        }
    }
}
