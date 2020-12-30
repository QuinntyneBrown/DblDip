using BuildingBlocks.Abstractions;
using DblDip.Core.Models;
using FluentValidation;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace DblDip.Domain.Features.EditedPhotos
{
    public class UpdateEditedPhoto
    {
        public class Validator : AbstractValidator<Request>
        {
            public Validator()
            {
                RuleFor(request => request.EditedPhoto).NotNull();
                RuleFor(request => request.EditedPhoto).SetValidator(new EditedPhotoValidator());
            }
        }

        public class Request : IRequest<Response>
        {
            public EditedPhotoDto EditedPhoto { get; init; }
        }

        public class Response
        {
            public EditedPhotoDto EditedPhoto { get; init; }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            private readonly IAppDbContext _context;

            public Handler(IAppDbContext context) => _context = context;

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {

                var editedPhoto = await _context.FindAsync<EditedPhoto>(request.EditedPhoto.EditedPhotoId);

                editedPhoto.Update();

                _context.Store(editedPhoto);

                await _context.SaveChangesAsync(cancellationToken);

                return new Response()
                {
                    EditedPhoto = editedPhoto.ToDto()
                };
            }
        }
    }
}
