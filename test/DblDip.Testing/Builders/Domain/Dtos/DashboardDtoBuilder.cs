using DblDip.Domain.Features.Dashboards;

namespace DblDip.Testing.Builders
{
    public class DashboardDtoBuilder
    {
        private DashboardDto _dashboardDto;

        public static DashboardDto WithDefaults()
        {
            return new DashboardDto()
            {
                Name = "Default",
                ProfileId = new System.Guid()
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
