using BuildingBlocks.Core;
using BuildingBlocks.EventStore;
using DblDip.Core.Models;
using FluentValidation;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace DblDip.Domain.Features
{
    public class CreatePhotoGallery
    {
        public class Validator : AbstractValidator<Request>
        {
            public Validator()
            {
                RuleFor(request => request.PhotoGallery).NotNull();
                RuleFor(request => request.PhotoGallery).SetValidator(new PhotoGalleryValidator());
            }
        }

        public class Request : IRequest<Response>
        {
            public PhotoGalleryDto PhotoGallery { get; init; }
        }

        public class Response: ResponseBase
        {
            public PhotoGalleryDto PhotoGallery { get; init; }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            private readonly IEventStore _store;

            public Handler(IEventStore store) => _store = store;

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {

                var photoGallery = new PhotoGallery(default);

                _store.Add(photoGallery);

                await _store.SaveChangesAsync(cancellationToken);

                return new Response()
                {
                    PhotoGallery = photoGallery.ToDto()
                };
            }
        }
    }
}
