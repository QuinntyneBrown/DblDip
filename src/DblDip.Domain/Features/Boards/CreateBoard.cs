using BuildingBlocks.Abstractions;
using DblDip.Core.Models;
using FluentValidation;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace DblDip.Domain.Features.Boards
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
            public BoardDto Board { get; set; }
        }

        public class Response
        {
            public BoardDto Board { get; set; }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            private readonly IAppDbContext _context;

            public Handler(IAppDbContext context) => _context = context;

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {

                var board = new Board();

                _context.Store(board);

                await _context.SaveChangesAsync(cancellationToken);

                return new Response()
                {
                    Board = board.ToDto()
                };
            }
        }
    }
}
