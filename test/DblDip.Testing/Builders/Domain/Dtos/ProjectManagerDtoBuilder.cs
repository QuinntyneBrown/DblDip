using DblDip.Core.Models;
using DblDip.Domain.Features.ProjectManagers;

namespace DblDip.Testing.Builders.Domain.Dtos
{
    public class ProjectManagerDtoBuilder
    {
        private ProjectManagerDto _projectManagerDto;

        public static ProjectManagerDto WithDefaults()
        {
            return new ProjectManagerDto();
        }

        public ProjectManagerDtoBuilder()
        {
            _projectManagerDto = WithDefaults();
        }

        public ProjectManagerDto Build()
        {
            return _projectManagerDto;
        }
    }
}
