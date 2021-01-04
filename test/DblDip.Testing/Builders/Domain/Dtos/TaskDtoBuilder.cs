using DblDip.Domain.Features.Tasks;

namespace DblDip.Testing.Builders
{
    public class TaskDtoBuilder
    {
        private TaskDto _taskDto;

        public static TaskDto WithDefaults()
        {
            return new TaskDto(default, default, default);
        }

        public TaskDtoBuilder()
        {
            _taskDto = WithDefaults();
        }

        public TaskDto Build()
        {
            return _taskDto;
        }
    }
}
