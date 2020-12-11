using FluentValidation;
using MediatR;
using BuildingBlocks.Abstractions;
using ShootQ.Core.Models;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.AspNetCore.Http;
using System;

namespace ShootQ.Domain.Features.Photographers
{
    public class ChangePhotographerAvatar
    {
        public class Validator : AbstractValidator<Request>
        {
            public Validator()
            {

            }
        }

        public class Request : IRequest<Response> {
            public Guid PhotographerId { get; set; }
        }

        public class Response
        {
            public Guid DigitalAssetId { get; set; }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            private readonly IAppDbContext _context;
            private readonly IHttpContextAccessor _httpContextAccessor;

            public Handler(IAppDbContext context, IHttpContextAccessor httpContextAccessor) {            
                _context = context;
                _httpContextAccessor = httpContextAccessor;
            }

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken) {

                var photographer = await _context.FindAsync<Photographer>(request.PhotographerId);

                var digitalAsset = (await DigitalAsset.Upload(_httpContextAccessor, _context, cancellationToken)).Single();

                photographer.ChangeAvatar(digitalAsset.DigitalAssetId);

                await _context.SaveChangesAsync(cancellationToken);

                return new Response()
                {
                    DigitalAssetId = digitalAsset.DigitalAssetId
                };
            }
        }
    }
}
