using BuildingBlocks.Abstractions;
using ShootQ.Core.Models;
using FluentValidation;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace ShootQ.Domain.Features.Tasks
{
    public class CreateTask
    {
        public class Validator : AbstractValidator<Request>
        {
            public Validator()
            {
                RuleFor(request => request.Task).NotNull();
                RuleFor(request => request.Task).SetValidator(new TaskValidator());
            }
        }

        public class Request : IRequest<Response> {  
            public TaskDto Task { get; set; }
        }

        public class Response
        {
            public TaskDto Task { get; set; }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            private readonly IAppDbContext _context;

            public Handler(IAppDbContext context) => _context = context;

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken) {

                var task = new ShootQ.Core.Models.Task();

                _context.Store(task);

                await _context.SaveChangesAsync(cancellationToken);

                return new Response()
                {
                    Task = task.ToDto()
                };
            }
        }
    }
}
