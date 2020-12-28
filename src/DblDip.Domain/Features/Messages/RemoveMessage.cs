using BuildingBlocks.Abstractions;
using DblDip.Core.Models;
using FluentValidation;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using System;

namespace DblDip.Domain.Features.Messages
{
    public class RemoveMessage
    {
        public class Validator : AbstractValidator<Request>
        {
            public Validator()
            {

            }
        }

        public class Request : IRequest<Unit> {  
            public Guid MessageId { get; set; }
        }

        public class Response
        {
            public MessageDto Message { get; set; }
        }

        public class Handler : IRequestHandler<Request, Unit>
        {
            private readonly IAppDbContext _context;
            private readonly IDateTime _dateTime;

            public Handler(IAppDbContext context, IDateTime dateTime) => (_context, _dateTime) = (context, dateTime);

            public async Task<Unit> Handle(Request request, CancellationToken cancellationToken) {

                var message = await _context.FindAsync<Message>(request.MessageId);

                message.Remove(_dateTime.UtcNow);

                _context.Store(message);

                await _context.SaveChangesAsync(cancellationToken);

                return new Unit()
                {

                };
            }
        }
    }
}
