using DblDip.Core.Models;

namespace DblDip.Testing.Builders.Core.Models
{
    public class SystemLocationBuilder
    {
        private SystemLocation _systemLocation;

        public static SystemLocation WithDefaults()
        {
            return new SystemLocation(default, default);
        }

        public SystemLocationBuilder()
        {
            _systemLocation = WithDefaults();
        }

        public SystemLocation Build()
        {
            return _systemLocation;
        }
    }
}
