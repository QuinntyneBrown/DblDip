using ShootQ.Core.Models;
using ShootQ.Domain.Features.ProjectManagers;

namespace ShootQ.Testing.Builders.Domain.Dtos
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
