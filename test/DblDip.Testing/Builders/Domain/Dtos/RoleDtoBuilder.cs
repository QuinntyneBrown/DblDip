using DblDip.Domain.Features.Roles;

namespace DblDip.Testing.Builders.Domain.Dtos
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
