using BuildingBlocks.Abstractions;
using DblDip.Core.Models;
using FluentValidation;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace DblDip.Domain.Features.Conversations
{
    public class UpdateConversation
    {
        public class Validator : AbstractValidator<Request>
        {
            public Validator()
            {
                RuleFor(request => request.Conversation).NotNull();
                RuleFor(request => request.Conversation).SetValidator(new ConversationValidator());
            }
        }

        public class Request : IRequest<Response>
        {
            public ConversationDto Conversation { get; set; }
        }

        public class Response
        {
            public ConversationDto Conversation { get; set; }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            private readonly IAppDbContext _context;

            public Handler(IAppDbContext context) => _context = context;

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {

                var conversation = await _context.FindAsync<Conversation>(request.Conversation.ConversationId);

                conversation.Update();

                _context.Store(conversation);

                await _context.SaveChangesAsync(cancellationToken);

                return new Response()
                {
                    Conversation = conversation.ToDto()
                };
            }
        }
    }
}
