using ShootQ.Domain.Features.Tasks;

namespace ShootQ.Testing.Builders.Domain.Dtos
{
    public class TaskDtoBuilder
    {
        private TaskDto _taskDto;

        public static TaskDto WithDefaults()
        {
            return new TaskDto();
        }

        public TaskDtoBuilder()
        {
            _taskDto = new TaskDto();
        }

        public TaskDto Build()
        {
            return _taskDto;
        }
    }
}
