using DblDip.Core.Models;

namespace DblDip.Testing.Builders
{
    public class SystemAdministratorBuilder
    {
        private SystemAdministrator _systemAdministrator;

        public static SystemAdministrator WithDefaults()
        {
            return new SystemAdministrator(default, default);
        }

        public SystemAdministratorBuilder()
        {
            _systemAdministrator = WithDefaults();
        }

        public SystemAdministrator Build()
        {
            return _systemAdministrator;
        }
    }
}
