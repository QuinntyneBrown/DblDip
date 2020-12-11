using Newtonsoft.Json;
using ShootQ.Core.Models;
using ShootQ.Domain.Features;
using ShootQ.Domain.Features.Services;
using ShootQ.Testing;
using ShootQ.Testing.Builders.Core.Models;
using ShootQ.Testing.Builders.Domain.Dtos;
using System;
using System.Linq;
using System.Net.Http;
using System.Text;
using Xunit;
using static ShootQ.Api.FunctionalTests.Controllers.ServicesControllerTests.Endpoints;

namespace ShootQ.Api.FunctionalTests.Controllers
{
    public class ServicesControllerTests : IClassFixture<ApiTestFixture>
    {
        private readonly ApiTestFixture _fixture;
        public ServicesControllerTests(ApiTestFixture fixture)
        {
            _fixture = fixture;
        }


        [Fact]
        public async System.Threading.Tasks.Task Should_CreateService()
        {
            var context = _fixture.Context;

            var service = ServiceDtoBuilder.WithDefaults();

            StringContent stringContent = new StringContent(JsonConvert.SerializeObject(new { service }), Encoding.UTF8, "application/json");

            using var client = _fixture.CreateAuthenticatedClient();

            var httpResponseMessage = await client.PostAsync(Endpoints.Post.CreateService, stringContent);

            var response = JsonConvert.DeserializeObject<CreateService.Response>(await httpResponseMessage.Content.ReadAsStringAsync());

            var sut = context.FindAsync<Service>(response.Service.ServiceId);

            Assert.NotEqual(default, response.Service.ServiceId);
        }

        [Fact]
        public async System.Threading.Tasks.Task Should_RemoveService()
        {
            var service = ServiceBuilder.WithDefaults();

            var context = _fixture.Context;

            var client = _fixture.CreateAuthenticatedClient();

            context.Store(service);

            await context.SaveChangesAsync(default);

            var httpResponseMessage = await client.DeleteAsync(Delete.By(service.ServiceId));

            httpResponseMessage.EnsureSuccessStatusCode();

            var removedService = await context.FindAsync<Service>(service.ServiceId);

            Assert.NotEqual(default, removedService.Deleted);
        }

        [Fact]
        public async System.Threading.Tasks.Task Should_UpdateService()
        {
            var service = ServiceBuilder.WithDefaults();

            var context = _fixture.Context;

            context.Store(service);

            await context.SaveChangesAsync(default);

            StringContent stringContent = new StringContent(JsonConvert.SerializeObject(new { service = service.ToDto() }), Encoding.UTF8, "application/json");

            var httpResponseMessage = await _fixture.CreateAuthenticatedClient().PutAsync(Put.Update, stringContent);

            httpResponseMessage.EnsureSuccessStatusCode();

            var sut = await context.FindAsync<Service>(service.ServiceId);

        }

        [Fact]
        public async System.Threading.Tasks.Task Should_GetServices()
        {
            var service = ServiceBuilder.WithDefaults();

            var context = _fixture.Context;

            context.Store(service);

            await context.SaveChangesAsync(default);

            var httpResponseMessage = await _fixture.CreateAuthenticatedClient().GetAsync(Get.Services);

            httpResponseMessage.EnsureSuccessStatusCode();

            var response = JsonConvert.DeserializeObject<GetServices.Response>(await httpResponseMessage.Content.ReadAsStringAsync());

            Assert.True(response.Services.Any());
        }

        [Fact]
        public async System.Threading.Tasks.Task Should_GetServiceById()
        {
            var service = ServiceBuilder.WithDefaults();

            var context = _fixture.Context;

            context.Store(service);

            await context.SaveChangesAsync(default);

            var httpResponseMessage = await _fixture.CreateAuthenticatedClient().GetAsync(Get.By(service.ServiceId));

            httpResponseMessage.EnsureSuccessStatusCode();

            var response = JsonConvert.DeserializeObject<GetServiceById.Response>(await httpResponseMessage.Content.ReadAsStringAsync());

            Assert.NotNull(response);

        }

        internal static class Endpoints
        {
            public static class Post
            {
                public static string CreateService = "api/services";
            }

            public static class Put
            {
                public static string Update = "api/services";
            }

            public static class Delete
            {
                public static string By(Guid serviceId)
                {
                    return $"api/services/{serviceId}";
                }
            }

            public static class Get
            {
                public static string Services = "api/services";
                public static string By(Guid serviceId)
                {
                    return $"api/services/{serviceId}";
                }
            }
        }
    }
}
