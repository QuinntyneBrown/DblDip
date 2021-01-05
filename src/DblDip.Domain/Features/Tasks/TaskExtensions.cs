using DblDip.Core.Models;
using DblDip.Domain.Features;

namespace DblDip.Domain.Features
{
    public static class TaskExtensions
    {
        public static TaskDto ToDto(this Task task)
            => new(task.TaskId, task.Name, task.Description);
    }
}
