using ShootQ.Core.Models;

namespace ShootQ.Testing.Builders.Core.Models
{
    public class TaskBuilder
    {
        private Task _task;

        public static Task WithDefaults()
        {
            return new Task();
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
