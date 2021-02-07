using BuildingBlocks.Core;
using FluentValidation;
using MediatR;
using DblDip.Core.Data;
using DblDip.Core.Models;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.AspNetCore.Http;
using System;

namespace DblDip.Domain.Features
{
    public class ChangePhotographerAvatar
    {
        public class Validator : AbstractValidator<Request>
        {
            public Validator()
            {

            }
        }

        public class Request : IRequest<Response>
        {
            public Guid PhotographerId { get; init; }
        }

        public class Response: ResponseBase
        {
            public Guid DigitalAssetId { get; init; }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            private readonly IDblDipDbContext _context;
            private readonly IHttpContextAccessor _httpContextAccessor;

            public Handler(IDblDipDbContext context, IHttpContextAccessor httpContextAccessor)
            {
                _context = context;
                _httpContextAccessor = httpContextAccessor;
            }

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {

                var photographer = await _context.FindAsync<Photographer>(request.PhotographerId);

                var digitalAsset = (await DigitalAsset.Upload(_httpContextAccessor, _context, cancellationToken)).Single();

                photographer.UpdateAvatar(digitalAsset.DigitalAssetId);

                await _context.SaveChangesAsync(cancellationToken);

                return new Response()
                {
                    DigitalAssetId = digitalAsset.DigitalAssetId
                };
            }
        }
    }
}
