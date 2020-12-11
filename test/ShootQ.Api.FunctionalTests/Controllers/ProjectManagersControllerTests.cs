using Newtonsoft.Json;
using ShootQ.Core.Models;
using ShootQ.Domain.Features;
using ShootQ.Domain.Features.ProjectManagers;
using ShootQ.Testing;
using ShootQ.Testing.Builders.Core.Models;
using ShootQ.Testing.Builders.Domain.Dtos;
using System;
using System.Linq;
using System.Net.Http;
using System.Text;
using Xunit;
using static ShootQ.Api.FunctionalTests.Controllers.ProjectManagersControllerTests.Endpoints;

namespace ShootQ.Api.FunctionalTests.Controllers
{
    public class ProjectManagersControllerTests : IClassFixture<ApiTestFixture>
    {
        private readonly ApiTestFixture _fixture;
        public ProjectManagersControllerTests(ApiTestFixture fixture)
        {
            _fixture = fixture;
        }


        [Fact]
        public async System.Threading.Tasks.Task Should_CreateProjectManager()
        {
            var context = _fixture.Context;

            var projectManager = ProjectManagerDtoBuilder.WithDefaults();

            StringContent stringContent = new StringContent(JsonConvert.SerializeObject(new { projectManager }), Encoding.UTF8, "application/json");

            using var client = _fixture.CreateAuthenticatedClient();

            var httpResponseMessage = await client.PostAsync(Post.CreateProjectManager, stringContent);

            var response = JsonConvert.DeserializeObject<CreateProjectManager.Response>(await httpResponseMessage.Content.ReadAsStringAsync());

            var sut = context.FindAsync<ProjectManager>(response.ProjectManager.ProjectManagerId);

            Assert.NotEqual(default, response.ProjectManager.ProjectManagerId);
        }

        [Fact]
        public async System.Threading.Tasks.Task Should_RemoveProjectManager()
        {
            var projectManager = ProjectManagerBuilder.WithDefaults();

            var context = _fixture.Context;

            var client = _fixture.CreateAuthenticatedClient();

            context.Store(projectManager);

            await context.SaveChangesAsync(default);

            var httpResponseMessage = await client.DeleteAsync(Delete.By(projectManager.ProjectManagerId));

            httpResponseMessage.EnsureSuccessStatusCode();

            var removedProjectManager = await context.FindAsync<ProjectManager>(projectManager.ProjectManagerId);

            Assert.NotEqual(default, removedProjectManager.Deleted);
        }

        [Fact]
        public async System.Threading.Tasks.Task Should_UpdateProjectManager()
        {
            var projectManager = ProjectManagerBuilder.WithDefaults();

            var context = _fixture.Context;

            context.Store(projectManager);

            await context.SaveChangesAsync(default);

            StringContent stringContent = new StringContent(JsonConvert.SerializeObject(new { projectManager = projectManager.ToDto() }), Encoding.UTF8, "application/json");

            var httpResponseMessage = await _fixture.CreateAuthenticatedClient().PutAsync(Put.Update, stringContent);

            httpResponseMessage.EnsureSuccessStatusCode();

            var sut = await context.FindAsync<ProjectManager>(projectManager.ProjectManagerId);

        }

        [Fact]
        public async System.Threading.Tasks.Task Should_GetProjectManagers()
        {
            var projectManager = ProjectManagerBuilder.WithDefaults();

            var context = _fixture.Context;

            context.Store(projectManager);

            await context.SaveChangesAsync(default);

            var httpResponseMessage = await _fixture.CreateAuthenticatedClient().GetAsync(Get.ProjectManagers);

            httpResponseMessage.EnsureSuccessStatusCode();

            var response = JsonConvert.DeserializeObject<GetProjectManagers.Response>(await httpResponseMessage.Content.ReadAsStringAsync());

            Assert.True(response.ProjectManagers.Any());
        }

        [Fact]
        public async System.Threading.Tasks.Task Should_GetProjectManagerById()
        {
            var projectManager = ProjectManagerBuilder.WithDefaults();

            var context = _fixture.Context;

            context.Store(projectManager);

            await context.SaveChangesAsync(default);

            var httpResponseMessage = await _fixture.CreateAuthenticatedClient().GetAsync(Get.By(projectManager.ProjectManagerId));

            httpResponseMessage.EnsureSuccessStatusCode();

            var response = JsonConvert.DeserializeObject<GetProjectManagerById.Response>(await httpResponseMessage.Content.ReadAsStringAsync());

            Assert.NotNull(response);

        }

        internal static class Endpoints
        {
            public static class Post
            {
                public static string CreateProjectManager = "api/project-managers";
            }

            public static class Put
            {
                public static string Update = "api/project-managers";
            }

            public static class Delete
            {
                public static string By(Guid projectManagerId)
                {
                    return $"api/project-managers/{projectManagerId}";
                }
            }

            public static class Get
            {
                public static string ProjectManagers = "api/project-managers";
                public static string By(Guid projectManagerId)
                {
                    return $"api/project-managers/{projectManagerId}";
                }
            }
        }
    }
}
