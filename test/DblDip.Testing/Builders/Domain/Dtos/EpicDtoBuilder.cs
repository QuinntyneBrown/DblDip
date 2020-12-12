using DblDip.Core.Models;
using DblDip.Domain.Features.Epics;

namespace DblDip.Testing.Builders.Domain.Dtos
{
    public class EpicDtoBuilder
    {
        private EpicDto _epicDto;

        public static EpicDto WithDefaults()
        {
            return new EpicDto();
        }

        public EpicDtoBuilder()
        {
            _epicDto = WithDefaults();
        }

        public EpicDto Build()
        {
            return _epicDto;
        }
    }
}
