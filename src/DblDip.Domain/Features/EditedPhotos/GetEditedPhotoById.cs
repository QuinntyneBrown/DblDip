using DblDip.Core.Data;
using DblDip.Core.Models;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DblDip.Domain.Features
{
    public class GetEditedPhotoById
    {
        public class Request : IRequest<Response>
        {
            public Guid EditedPhotoId { get; init; }
        }

        public class Response
        {
            public EditedPhotoDto EditedPhoto { get; init; }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            private readonly IDblDipDbContext _context;

            public Handler(IDblDipDbContext context) => _context = context;

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {

                var editedPhoto = await _context.FindAsync<EditedPhoto>(request.EditedPhotoId);

                return new Response()
                {
                    EditedPhoto = editedPhoto.ToDto()
                };
            }
        }
    }
}
