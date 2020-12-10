using ShootQ.Core.ValueObjects;
using System;

namespace ShootQ.Domain.Features.Tasks
{
    public class TaskDto
    {
        public Guid TaskId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateRange Scheduled { get; set; }
    }
}
