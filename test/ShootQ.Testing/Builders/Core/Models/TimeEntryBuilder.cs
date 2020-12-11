using ShootQ.Core.Models;

namespace ShootQ.Testing.Builders.Core.Models
{
    public class TimeEntryBuilder
    {
        private TimeEntry _timeEntry;

        public static TimeEntry WithDefaults()
        {
            return new TimeEntry();
        }

        public TimeEntryBuilder()
        {
            _timeEntry = WithDefaults();
        }

        public TimeEntry Build()
        {
            return _timeEntry;
        }
    }
}
