using BuildingBlocks.Core;
using BuildingBlocks.EventStore;
using DblDip.Core.Models;
using FluentValidation;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace DblDip.Domain.Features
{
    public class UpdatePhotographer
    {
        public class Validator : AbstractValidator<Request>
        {
            public Validator()
            {
                RuleFor(request => request.Photographer).NotNull();
                RuleFor(request => request.Photographer).SetValidator(new PhotographerValidator());
            }
        }

        public class Request : IRequest<Response>
        {
            public PhotographerDto Photographer { get; init; }
        }

        public class Response: ResponseBase
        {
            public PhotographerDto Photographer { get; init; }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            private readonly IEventStore _store;

            public Handler(IEventStore store) => _store = store;

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {

                var photographer = await _store.FindAsync<Photographer>(request.Photographer.PhotographerId);

                photographer.Update();

                _store.Add(photographer);

                await _store.SaveChangesAsync(cancellationToken);

                return new Response()
                {
                    Photographer = photographer.ToDto()
                };
            }
        }
    }
}
