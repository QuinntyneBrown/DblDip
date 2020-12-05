using BuildingBlocks.Core;
using ShootQ.Core.Models;
using ShootQ.Domain.Features.Dashboards;
using ShootQ.Testing;
using ShootQ.Testing.Builders.Core.Models;
using ShootQ.Testing.Builders.Domain.Dtos;
using System;
using Xunit;

namespace ShootQ.Api.FunctionalTests.Controllers
{
    public class DashboardsControllerTests: IClassFixture<ApiTestFixture>
    {
        private readonly ApiTestFixture _fixture;
        public DashboardsControllerTests(ApiTestFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public async System.Threading.Tasks.Task Should_AddDashboard()
        {
            var dashboard = DashboardDtoBuilder.WithDefaults();

            var client = _fixture.CreateAuthenticatedClient();

            var response = await client.PostAsAsync<dynamic, CreateDashboard.Response>(Endpoints.Post.AddDashboard, new { dashboard });

            var sut = await _fixture.Context.FindAsync<Dashboard>(response.Dashboard.DashboardId);

            Assert.NotEqual(default, sut.DashboardId);
        }

        [Fact]
        public async System.Threading.Tasks.Task Should_RemoveDashboard()
        {   
            var dashboard = _fixture.Context.Store(DashboardBuilder.WithDefaults(Guid.NewGuid()));

            await _fixture.Context.SaveChangesAsync(default);

            var client = _fixture.CreateAuthenticatedClient();

            var httpResponseMessage = await client.DeleteAsync(Endpoints.Delete.DashboardBy(dashboard.DashboardId));

            httpResponseMessage.EnsureSuccessStatusCode();

            var sut = await _fixture.Context.FindAsync<Dashboard>(dashboard.DashboardId);

            Assert.True(sut.Deleted.HasValue);
        }

        internal static class Endpoints
        {
            public static class Post
            {
                public static string AddDashboard = "api/dashboards";
            }

            public static class Delete
            {
                public static string DashboardBy(Guid dashboardId)
                {
                    return $"api/dashboards/{dashboardId}";
                }
            }
        }
    }
}