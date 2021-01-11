using BuildingBlocks.Core;
using DblDip.Core.Models;
using DblDip.Core.ValueObjects;
using DblDip.Domain.Features;
using DblDip.Testing;
using DblDip.Testing.Builders;
using DblDip.Testing.Builders;
using System;
using Xunit;
using Xunit.Abstractions;

namespace DblDip.Api.FunctionalTests
{
    public class DashboardsControllerTests : IClassFixture<ApiTestFixture>
    {
        private readonly ApiTestFixture _fixture;
        private readonly ITestOutputHelper _testOutputHelper;
        public DashboardsControllerTests(ApiTestFixture fixture, ITestOutputHelper testOutputHelper)
        {
            _fixture = fixture;
            _testOutputHelper = testOutputHelper;
        }

        [Fact]
        public async System.Threading.Tasks.Task Should_CreateDashboard()
        {
            var dashboard = DashboardDtoBuilder.WithDefaults();

            using var client = _fixture.CreateAuthenticatedClient();

            var response = await client.PostAsAsync<dynamic, CreateDashboard.Response>(Endpoints.Post.Create, new { dashboard });

            _testOutputHelper.WriteLine($"{response.Dashboard.DashboardId}");

            var sut = await _fixture.Context.FindAsync<Dashboard>(response.Dashboard.DashboardId);

            Assert.NotEqual(default, sut.DashboardId);
        }

        [Fact]
        public async System.Threading.Tasks.Task Should_GetDefaultDashboard()
        {
            var profile = new Lead((Email)"quinntynebrown@gmail.com");

            var homeDashboard = new Dashboard("Home", profile.ProfileId);

            var reportingDashboard = new Dashboard("Reporting", profile.ProfileId);

            homeDashboard.SetDefault();

            _fixture.Context.Add(profile);

            _fixture.Context.Add(homeDashboard);

            _fixture.Context.Add(reportingDashboard);

            await _fixture.Context.SaveChangesAsync(default);

            using var client = _fixture.CreateAuthenticatedClient();

            var response = await client.PostAsAsync<dynamic, CreateDashboard.Response>(Endpoints.Post.Create, new { homeDashboard });

            _testOutputHelper.WriteLine($"{response.Dashboard.DashboardId}");

            var sut = await _fixture.Context.FindAsync<Dashboard>(response.Dashboard.DashboardId);

            Assert.NotEqual(default, sut.DashboardId);
        }


        [Fact]
        public async System.Threading.Tasks.Task Should_RemoveDashboard()
        {
            var dashboard = DashboardBuilder.WithDefaults(Guid.NewGuid());

            _fixture.Context.Add(dashboard);

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
                public static string Create = "api/dashboards";
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
