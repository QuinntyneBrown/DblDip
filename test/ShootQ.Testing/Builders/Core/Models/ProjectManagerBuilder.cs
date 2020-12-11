using ShootQ.Core.Models;

namespace ShootQ.Testing.Builders.Core.Models
{
    public class ProjectManagerBuilder
    {
        private ProjectManager _projectManager;

        public static ProjectManager WithDefaults()
        {
            return new ProjectManager(default, default);
        }

        public ProjectManagerBuilder()
        {
            _projectManager = WithDefaults();
        }

        public ProjectManager Build()
        {
            return _projectManager;
        }
    }
}
