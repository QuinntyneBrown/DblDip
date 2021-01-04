using DblDip.Domain.Features;

namespace DblDip.Testing.Builders
{
    public class RoleDtoBuilder
    {
        private RoleDto _roleDto;

        public static RoleDto WithDefaults()
        {
            return new RoleDto(default, default, default);
        }

        public RoleDtoBuilder()
        {
            _roleDto = WithDefaults();
        }

        public RoleDto Build()
        {
            return _roleDto;
        }
    }
}
