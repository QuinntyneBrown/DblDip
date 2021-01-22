using Microsoft.EntityFrameworkCore;
using System;

namespace DblDip.Core.Models
{
    [Owned]
    public class TaskReference
    {
        public Guid TaskId { get; set; }

        public TaskReference(Guid taskId)
        {
            TaskId = taskId;
        }

        public TaskReference()
        {

        }
    }
}
