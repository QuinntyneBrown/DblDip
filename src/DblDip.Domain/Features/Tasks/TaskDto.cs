using DblDip.Core.ValueObjects;
using System;

namespace DblDip.Domain.Features.Tasks
{
    public class TaskDto
    {
        public Guid TaskId { get; init; }
        public string Name { get; init; }
        public string Description { get; init; }
        public DateRange Scheduled { get; init; }
    }
}
