using BuildingBlocks.EventStore;
using DblDip.Core.Models;
using FluentValidation;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace DblDip.Domain.Features
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
            public EpicDto Epic { get; init; }
        }

        public class Response
        {
            public EpicDto Epic { get; init; }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            private readonly IEventStore _store;

            public Handler(IEventStore store) => _store = store;

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {

                var epic = new Epic();

                _store.Add(epic);

                await _store.SaveChangesAsync(cancellationToken);

                return new Response()
                {
                    Epic = epic.ToDto()
                };
            }
        }
    }
}
