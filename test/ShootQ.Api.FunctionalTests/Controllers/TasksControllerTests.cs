using Newtonsoft.Json;
using ShootQ.Core.Models;
using ShootQ.Domain.Features;
using ShootQ.Domain.Features.Tasks;
using ShootQ.Testing;
using ShootQ.Testing.Builders.Core.Models;
using ShootQ.Testing.Builders.Domain.Dtos;
using System;
using System.Linq;
using System.Net.Http;
using System.Text;
using Xunit;
using static ShootQ.Api.FunctionalTests.Controllers.TasksControllerTests.Endpoints;

namespace ShootQ.Api.FunctionalTests.Controllers
{
    public class TasksControllerTests : IClassFixture<ApiTestFixture>
    {
        private readonly ApiTestFixture _fixture;
        public TasksControllerTests(ApiTestFixture fixture)
        {
            _fixture = fixture;
        }


        [Fact]
        public async System.Threading.Tasks.Task Should_CreateTask()
        {
            var context = _fixture.Context;

            var task = TaskDtoBuilder.WithDefaults();

            StringContent stringContent = new StringContent(JsonConvert.SerializeObject(new { task }), Encoding.UTF8, "application/json");

            using var client = _fixture.CreateAuthenticatedClient();

            var httpResponseMessage = await client.PostAsync(Post.CreateTask, stringContent);

            var response = JsonConvert.DeserializeObject<CreateTask.Response>(await httpResponseMessage.Content.ReadAsStringAsync());

            var sut = context.FindAsync<Task>(response.Task.TaskId);

            Assert.NotEqual(default, response.Task.TaskId);
        }

        [Fact]
        public async System.Threading.Tasks.Task Should_RemoveTask()
        {
            var task = TaskBuilder.WithDefaults();

            var context = _fixture.Context;

            var client = _fixture.CreateAuthenticatedClient();

            context.Store(task);

            await context.SaveChangesAsync(default);

            var httpResponseMessage = await client.DeleteAsync(Delete.By(task.TaskId));

            httpResponseMessage.EnsureSuccessStatusCode();

            var removedTask = await context.FindAsync<Task>(task.TaskId);

            Assert.NotEqual(default, removedTask.Deleted);
        }

        [Fact]
        public async System.Threading.Tasks.Task Should_UpdateTask()
        {
            var task = TaskBuilder.WithDefaults();

            var context = _fixture.Context;

            context.Store(task);

            await context.SaveChangesAsync(default);

            StringContent stringContent = new StringContent(JsonConvert.SerializeObject(new { task = task.ToDto() }), Encoding.UTF8, "application/json");

            var httpResponseMessage = await _fixture.CreateAuthenticatedClient().PutAsync(Put.Update, stringContent);

            httpResponseMessage.EnsureSuccessStatusCode();

            var sut = await context.FindAsync<Task>(task.TaskId);

        }

        [Fact]
        public async System.Threading.Tasks.Task Should_GetTasks()
        {
            var task = TaskBuilder.WithDefaults();

            var context = _fixture.Context;

            context.Store(task);

            await context.SaveChangesAsync(default);

            var httpResponseMessage = await _fixture.CreateAuthenticatedClient().GetAsync(Get.tasks);

            httpResponseMessage.EnsureSuccessStatusCode();

            var response = JsonConvert.DeserializeObject<GetTasks.Response>(await httpResponseMessage.Content.ReadAsStringAsync());

            Assert.True(response.Tasks.Any());
        }

        [Fact]
        public async System.Threading.Tasks.Task Should_GetTaskById()
        {
            var task = TaskBuilder.WithDefaults();

            var context = _fixture.Context;

            context.Store(task);

            await context.SaveChangesAsync(default);

            var httpResponseMessage = await _fixture.CreateAuthenticatedClient().GetAsync(Get.By(task.TaskId));

            httpResponseMessage.EnsureSuccessStatusCode();

            var response = JsonConvert.DeserializeObject<GetTaskById.Response>(await httpResponseMessage.Content.ReadAsStringAsync());

            Assert.NotNull(response);

        }

        internal static class Endpoints
        {
            public static class Post
            {
                public static string CreateTask = "api/tasks";
            }

            public static class Put
            {
                public static string Update = "api/tasks";
            }

            public static class Delete
            {
                public static string By(Guid taskId)
                {
                    return $"api/tasks/{taskId}";
                }
            }

            public static class Get
            {
                public static string tasks = "api/tasks";
                public static string By(Guid taskId)
                {
                    return $"api/tasks/{taskId}";
                }
            }
        }
    }
}
