using ShootQ.Core.Models;
using static ShootQ.Core.Models.Role;

namespace ShootQ.Testing.Builders.Core.Models
{
    public class PrivilegeBuilder
    {
        private Privilege _privilege;

        public static Privilege WithDefaults()
        {
            return new Privilege(default,default);
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
