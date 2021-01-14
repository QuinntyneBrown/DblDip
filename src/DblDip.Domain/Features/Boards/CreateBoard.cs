using BuildingBlocks.EventStore;
using DblDip.Core.Models;
using FluentValidation;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace DblDip.Domain.Features
{
    public class CreateBoard
    {
        public class Validator : AbstractValidator<Request>
        {
            public Validator()
            {
                RuleFor(request => request.Board).NotNull();
                RuleFor(request => request.Board).SetValidator(new BoardValidator());
            }
        }

        public class Request : IRequest<Response>
        {
            public BoardDto Board { get; init; }
        }

        public class Response
        {
            public BoardDto Board { get; init; }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            private readonly IEventStore _store;

            public Handler(IEventStore store) => _store = store;

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {

                var board = new Board(default);

                _store.Add(board);

                await _store.SaveChangesAsync(cancellationToken);

                return new Response()
                {
                    Board = board.ToDto()
                };
            }
        }
    }
}
