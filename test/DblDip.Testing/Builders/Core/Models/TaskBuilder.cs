using DblDip.Core.Models;

namespace DblDip.Testing.Builders
{
    public class TaskBuilder
    {
        private Task _task;

        public static Task WithDefaults()
        {
            return new Task(default, default);
        }

        public TaskBuilder()
        {
            _task = WithDefaults();
        }

        public Task Build()
        {
            return _task;
        }
    }
}
