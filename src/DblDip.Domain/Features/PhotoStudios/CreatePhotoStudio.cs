using BuildingBlocks.EventStore;
using DblDip.Core.Models;
using FluentValidation;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace DblDip.Domain.Features
{
    public class CreatePhotoStudio
    {
        public class Validator : AbstractValidator<Request>
        {
            public Validator()
            {
                RuleFor(request => request.PhotoStudio).NotNull();
                RuleFor(request => request.PhotoStudio).SetValidator(new PhotoStudioValidator());
            }
        }

        public class Request : IRequest<Response>
        {
            public PhotoStudioDto PhotoStudio { get; init; }
        }

        public class Response
        {
            public PhotoStudioDto PhotoStudio { get; init; }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            private readonly IEventStore _store;

            public Handler(IEventStore store) => _store = store;

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {

                var photoStudio = new PhotoStudio();

                _store.Add(photoStudio);

                await _store.SaveChangesAsync(cancellationToken);

                return new Response()
                {
                    PhotoStudio = photoStudio.ToDto()
                };
            }
        }
    }
}
