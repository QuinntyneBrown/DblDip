using DblDip.Core.Data;
using DblDip.Core.Models;
using FluentValidation;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace DblDip.Domain.Features
{
    public class UpdateEpic
    {
        public class Validator : AbstractValidator<Request>
        {
            public Validator()
            {
                RuleFor(request => request.Epic).NotNull();
                RuleFor(request => request.Epic).SetValidator(new EpicValidator());
            }
        }

        public class Request : IRequest<Response>
        {
            public EpicDto Epic { get; init; }
        }

        public class Response
        {
            public EpicDto Epic { get; init; }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            private readonly IDblDipDbContext _context;

            public Handler(IDblDipDbContext context) => _context = context;

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {

                var epic = await _context.FindAsync<Epic>(request.Epic.EpicId);

                epic.Update();

                _context.Add(epic);

                await _context.SaveChangesAsync(cancellationToken);

                return new Response()
                {
                    Epic = epic.ToDto()
                };
            }
        }
    }
}
