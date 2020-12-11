using ShootQ.Core.Models;
using ShootQ.Domain.Features.SystemAdministrators;

namespace ShootQ.Testing.Builders.Domain.Dtos
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
