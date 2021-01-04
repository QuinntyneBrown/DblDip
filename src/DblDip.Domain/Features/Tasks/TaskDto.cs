using DblDip.Core.ValueObjects;
using System;

namespace DblDip.Domain.Features
{
    public class TaskDto
    {
        public TaskDto(Guid taskId, string name, string description)
        {
            TaskId = taskId;
            Name = name;
            Description = description;

        }

        public Guid TaskId { get; init; }        
        public Guid OwnerId { get; init; }
        public Guid? ProjectId { get; set; }
        public string Name { get; init; }
        public string Description { get; init; }
        public DateRange Scheduled { get; init; }
    }
}
