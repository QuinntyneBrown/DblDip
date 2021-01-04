using DblDip.Core.Models;
using DblDip.Domain.Features;

namespace DblDip.Testing.Builders
{
    public class SystemAdministratorDtoBuilder
    {
        private SystemAdministratorDto _systemAdministratorDto;

        public static SystemAdministratorDto WithDefaults()
        {
            return new SystemAdministratorDto();
        }

        public SystemAdministratorDtoBuilder()
        {
            _systemAdministratorDto = WithDefaults();
        }

        public SystemAdministratorDto Build()
        {
            return _systemAdministratorDto;
        }
    }
}
