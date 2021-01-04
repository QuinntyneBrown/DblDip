using DblDip.Core.Models;

namespace DblDip.Testing.Builders
{
    public class RoleBuilder
    {
        private Role _role;

        public static Role WithDefaults()
        {
            return new Role("Test");
        }

        public RoleBuilder()
        {
            _role = WithDefaults();
        }

        public Role Build()
        {
            return _role;
        }
    }
}
