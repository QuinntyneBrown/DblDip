using BuildingBlocks.Abstractions;
using DblDip.Core.Models;
using FluentValidation;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace DblDip.Domain.Features.Tasks
{
    public class UpdateTask
    {
        public class Validator : AbstractValidator<Request>
        {
            public Validator()
            {
                RuleFor(request => request.Task).NotNull();
                RuleFor(request => request.Task).SetValidator(new TaskValidator());
            }
        }

        public class Request : IRequest<Response>
        {
            public TaskDto Task { get; init; }
        }

        public class Response
        {
            public TaskDto Task { get; init; }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            private readonly IAppDbContext _context;

            public Handler(IAppDbContext context) => _context = context;

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {

                var task = await _context.FindAsync<DblDip.Core.Models.Task>(request.Task.TaskId);

                task.Update();

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
