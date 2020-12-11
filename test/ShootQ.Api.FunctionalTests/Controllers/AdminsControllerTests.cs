using Newtonsoft.Json;
using ShootQ.Core.Models;
using ShootQ.Domain.Features;
using ShootQ.Domain.Features.Admins;
using ShootQ.Testing;
using ShootQ.Testing.Builders.Core.Models;
using ShootQ.Testing.Builders.Domain.Dtos;
using System;
using System.Linq;
using System.Net.Http;
using System.Text;
using Xunit;
using static ShootQ.Api.FunctionalTests.Controllers.AdminsControllerTests.Endpoints;

namespace ShootQ.Api.FunctionalTests.Controllers
{
    public class AdminsControllerTests : IClassFixture<ApiTestFixture>
    {
        private readonly ApiTestFixture _fixture;
        public AdminsControllerTests(ApiTestFixture fixture)
        {
            _fixture = fixture;
        }


        [Fact]
        public async System.Threading.Tasks.Task Should_CreateAdmin()
        {
            var context = _fixture.Context;

            var admin = AdminDtoBuilder.WithDefaults();

            StringContent stringContent = new StringContent(JsonConvert.SerializeObject(new { admin }), Encoding.UTF8, "application/json");

            using var client = _fixture.CreateAuthenticatedClient();

            var httpResponseMessage = await client.PostAsync(Post.CreateAdmin, stringContent);

            var response = JsonConvert.DeserializeObject<CreateAdmin.Response>(await httpResponseMessage.Content.ReadAsStringAsync());

            var sut = context.FindAsync<Admin>(response.Admin.AdminId);

            Assert.NotEqual(default, response.Admin.AdminId);
        }

        [Fact]
        public async System.Threading.Tasks.Task Should_RemoveAdmin()
        {
            var admin = AdminBuilder.WithDefaults();

            var context = _fixture.Context;

            var client = _fixture.CreateAuthenticatedClient();

            context.Store(admin);

            await context.SaveChangesAsync(default);

            var httpResponseMessage = await client.DeleteAsync(Delete.By(admin.AdminId));

            httpResponseMessage.EnsureSuccessStatusCode();

            var removedAdmin = await context.FindAsync<Admin>(admin.AdminId);

            Assert.NotEqual(default, removedAdmin.Deleted);
        }

        [Fact]
        public async System.Threading.Tasks.Task Should_UpdateAdmin()
        {
            var admin = AdminBuilder.WithDefaults();

            var context = _fixture.Context;

            context.Store(admin);

            await context.SaveChangesAsync(default);

            StringContent stringContent = new StringContent(JsonConvert.SerializeObject(new { admin = admin.ToDto() }), Encoding.UTF8, "application/json");

            var httpResponseMessage = await _fixture.CreateAuthenticatedClient().PutAsync(Put.Update, stringContent);

            httpResponseMessage.EnsureSuccessStatusCode();

            var sut = await context.FindAsync<Admin>(admin.AdminId);

        }

        [Fact]
        public async System.Threading.Tasks.Task Should_GetAdmins()
        {
            var admin = AdminBuilder.WithDefaults();

            var context = _fixture.Context;

            context.Store(admin);

            await context.SaveChangesAsync(default);

            var httpResponseMessage = await _fixture.CreateAuthenticatedClient().GetAsync(Get.Admins);

            httpResponseMessage.EnsureSuccessStatusCode();

            var response = JsonConvert.DeserializeObject<GetAdmins.Response>(await httpResponseMessage.Content.ReadAsStringAsync());

            Assert.True(response.Admins.Any());
        }

        [Fact]
        public async System.Threading.Tasks.Task Should_GetAdminById()
        {
            var admin = AdminBuilder.WithDefaults();

            var context = _fixture.Context;

            context.Store(admin);

            await context.SaveChangesAsync(default);

            var httpResponseMessage = await _fixture.CreateAuthenticatedClient().GetAsync(Get.By(admin.AdminId));

            httpResponseMessage.EnsureSuccessStatusCode();

            var response = JsonConvert.DeserializeObject<GetAdminById.Response>(await httpResponseMessage.Content.ReadAsStringAsync());

            Assert.NotNull(response);

        }

        internal static class Endpoints
        {
            public static class Post
            {
                public static string CreateAdmin = "api/admins";
            }

            public static class Put
            {
                public static string Update = "api/admins";
            }

            public static class Delete
            {
                public static string By(Guid adminId)
                {
                    return $"api/admins/{adminId}";
                }
            }

            public static class Get
            {
                public static string Admins = "api/admins";
                public static string By(Guid adminId)
                {
                    return $"api/admins/{adminId}";
                }
            }
        }
    }
}
