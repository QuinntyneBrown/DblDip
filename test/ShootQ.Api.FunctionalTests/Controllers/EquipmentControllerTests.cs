using Newtonsoft.Json;
using ShootQ.Core.Models;
using ShootQ.Domain.Features;
using ShootQ.Domain.Features.Equipment;
using ShootQ.Testing;
using ShootQ.Testing.Builders.Core.Models;
using ShootQ.Testing.Builders.Domain.Dtos;
using System;
using System.Linq;
using System.Net.Http;
using System.Text;
using Xunit;
using static ShootQ.Api.FunctionalTests.Controllers.EquipmentControllerTests.Endpoints;

namespace ShootQ.Api.FunctionalTests.Controllers
{
    public class EquipmentControllerTests : IClassFixture<ApiTestFixture>
    {
        private readonly ApiTestFixture _fixture;
        public EquipmentControllerTests(ApiTestFixture fixture)
        {
            _fixture = fixture;
        }


        [Fact]
        public async System.Threading.Tasks.Task Should_CreateEquipment()
        {
            var context = _fixture.Context;

            var equipment = EquipmentDtoBuilder.WithDefaults();

            StringContent stringContent = new StringContent(JsonConvert.SerializeObject(new { equipment }), Encoding.UTF8, "application/json");

            using var client = _fixture.CreateAuthenticatedClient();

            var httpResponseMessage = await client.PostAsync(Post.CreateEquipment, stringContent);

            var response = JsonConvert.DeserializeObject<CreateEquipment.Response>(await httpResponseMessage.Content.ReadAsStringAsync());

            var sut = context.FindAsync<Equipment>(response.Equipment.EquipmentId);

            Assert.NotEqual(default, response.Equipment.EquipmentId);
        }

        [Fact]
        public async System.Threading.Tasks.Task Should_RemoveEquipment()
        {
            var equipment = EquipmentBuilder.WithDefaults();

            var context = _fixture.Context;

            var client = _fixture.CreateAuthenticatedClient();

            context.Store(equipment);

            await context.SaveChangesAsync(default);

            var httpResponseMessage = await client.DeleteAsync(Delete.By(equipment.EquipmentId));

            httpResponseMessage.EnsureSuccessStatusCode();

            var removedEquipment = await context.FindAsync<Equipment>(equipment.EquipmentId);

            Assert.NotEqual(default, removedEquipment.Deleted);
        }

        [Fact]
        public async System.Threading.Tasks.Task Should_UpdateEquipment()
        {
            var equipment = EquipmentBuilder.WithDefaults();

            var context = _fixture.Context;

            context.Store(equipment);

            await context.SaveChangesAsync(default);

            StringContent stringContent = new StringContent(JsonConvert.SerializeObject(new { equipment = equipment.ToDto() }), Encoding.UTF8, "application/json");

            var httpResponseMessage = await _fixture.CreateAuthenticatedClient().PutAsync(Put.Update, stringContent);

            httpResponseMessage.EnsureSuccessStatusCode();

            var sut = await context.FindAsync<Equipment>(equipment.EquipmentId);

        }

        [Fact]
        public async System.Threading.Tasks.Task Should_GetEquipment()
        {
            var equipment = EquipmentBuilder.WithDefaults();

            var context = _fixture.Context;

            context.Store(equipment);

            await context.SaveChangesAsync(default);

            var httpResponseMessage = await _fixture.CreateAuthenticatedClient().GetAsync(Get.equipment);

            httpResponseMessage.EnsureSuccessStatusCode();

            var response = JsonConvert.DeserializeObject<GetEquipment.Response>(await httpResponseMessage.Content.ReadAsStringAsync());

            Assert.True(response.Equipment.Any());
        }

        [Fact]
        public async System.Threading.Tasks.Task Should_GetEquipmentById()
        {
            var equipment = EquipmentBuilder.WithDefaults();

            var context = _fixture.Context;

            context.Store(equipment);

            await context.SaveChangesAsync(default);

            var httpResponseMessage = await _fixture.CreateAuthenticatedClient().GetAsync(Get.By(equipment.EquipmentId));

            httpResponseMessage.EnsureSuccessStatusCode();

            var response = JsonConvert.DeserializeObject<GetEquipmentById.Response>(await httpResponseMessage.Content.ReadAsStringAsync());

            Assert.NotNull(response);

        }

        internal static class Endpoints
        {
            public static class Post
            {
                public static string CreateEquipment = "api/equipment";
            }

            public static class Put
            {
                public static string Update = "api/equipment";
            }

            public static class Delete
            {
                public static string By(Guid equipmentId)
                {
                    return $"api/equipment/{equipmentId}";
                }
            }

            public static class Get
            {
                public static string equipment = "api/equipment";
                public static string By(Guid equipmentId)
                {
                    return $"api/equipment/{equipmentId}";
                }
            }
        }
    }
}
