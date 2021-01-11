using DblDip.Core.Data;
using DblDip.Core.Models;
using FluentValidation;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace DblDip.Domain.Features
{
    public class UpdateMessage
    {
        public class Validator : AbstractValidator<Request>
        {
            public Validator()
            {
                RuleFor(request => request.Message).NotNull();
                RuleFor(request => request.Message).SetValidator(new MessageValidator());
            }
        }

        public class Request : IRequest<Response>
        {
            public MessageDto Message { get; set; }
        }

        public class Response
        {
            public MessageDto Message { get; set; }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            private readonly IDblDipDbContext _context;

            public Handler(IDblDipDbContext context) => _context = context;

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {

                var message = await _context.FindAsync<Message>(request.Message.MessageId);

                message.Update();

                _context.Add(message);

                await _context.SaveChangesAsync(cancellationToken);

                return new Response()
                {
                    Message = message.ToDto()
                };
            }
        }
    }
}
