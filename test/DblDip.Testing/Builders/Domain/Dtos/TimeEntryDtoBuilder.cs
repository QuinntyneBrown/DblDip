using DblDip.Core.Models;
using DblDip.Domain.Features.TimeEntries;

namespace DblDip.Testing.Builders.Domain.Dtos
{
    public class TimeEntryDtoBuilder
    {
        private TimeEntryDto _timeEntryDto;

        public static TimeEntryDto WithDefaults()
        {
            return new TimeEntryDto();
        }

        public TimeEntryDtoBuilder()
        {
            _timeEntryDto = WithDefaults();
        }

        public TimeEntryDto Build()
        {
            return _timeEntryDto;
        }
    }
}
