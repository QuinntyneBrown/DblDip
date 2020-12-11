using DblDip.Core.Models;
using DblDip.Domain.Features.SystemAdministrators;

namespace DblDip.Testing.Builders.Domain.Dtos
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
