using ShootQ.Domain.Features.Dashboards;

namespace ShootQ.Testing.Builders.Domain.Dtos
{
    public class DashboardDtoBuilder
    {
        private DashboardDto _dashboardDto;

        public static DashboardDto WithDefaults()
        {
            return new DashboardDto()
            {
                Name = "Default",
                UserId = new System.Guid()
            };
        }

        public DashboardDtoBuilder()
        {
            _dashboardDto = new DashboardDto();
        }

        public DashboardDto Build()
        {
            return _dashboardDto;
        }
    }
}
