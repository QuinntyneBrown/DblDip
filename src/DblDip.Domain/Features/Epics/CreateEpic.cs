using BuildingBlocks.Abstractions;
using DblDip.Core.Models;
using FluentValidation;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace DblDip.Domain.Features.Epics
{
    public class CreateEpic
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
            public EpicDto Epic { get; set; }
        }

        public class Response
        {
            public EpicDto Epic { get; set; }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            private readonly IAppDbContext _context;

            public Handler(IAppDbContext context) => _context = context;

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {

                var epic = new Epic();

                _context.Store(epic);

                await _context.SaveChangesAsync(cancellationToken);

                return new Response()
                {
                    Epic = epic.ToDto()
                };
            }
        }
    }
}
