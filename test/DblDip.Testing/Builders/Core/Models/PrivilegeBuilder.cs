using DblDip.Core.Models;
using static DblDip.Core.Models.Role;

namespace DblDip.Testing.Builders
{
    public class PrivilegeBuilder
    {
        private Privilege _privilege;

        public static Privilege WithDefaults()
        {
            return new Privilege(default, default);
        }

        public PrivilegeBuilder()
        {
            _privilege = WithDefaults();
        }

        public Privilege Build()
        {
            return _privilege;
        }
    }
}
