using BuildingBlocks.Abstractions;
using ShootQ.Core.Models;
using FluentValidation;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace ShootQ.Domain.Features.Admins
{
    public class UpdateAdmin
    {
        public class Validator : AbstractValidator<Request>
        {
            public Validator()
            {
                RuleFor(request => request.Admin).NotNull();
                RuleFor(request => request.Admin).SetValidator(new AdminValidator());
            }
        }

        public class Request : IRequest<Response> {  
            public AdminDto Admin { get; set; }
        }

        public class Response
        {
            public AdminDto Admin { get; set; }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            private readonly IAppDbContext _context;

            public Handler(IAppDbContext context) => _context = context;

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken) {

                var admin = await _context.FindAsync<Admin>(request.Admin.AdminId);

                //admin.Update();

                _context.Store(admin);

                await _context.SaveChangesAsync(cancellationToken);

                return new Response()
                {
                    Admin = admin.ToDto()
                };
            }
        }
    }
}
