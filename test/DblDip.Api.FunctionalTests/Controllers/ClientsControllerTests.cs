using Newtonsoft.Json;
using DblDip.Core.Models;
using DblDip.Domain.Features;
using DblDip.Domain.Features;
using DblDip.Testing;
using DblDip.Testing.Builders;
using DblDip.Testing.Builders;
using System;
using System.Linq;
using System.Net.Http;
using System.Text;
using Xunit;
using static DblDip.Api.FunctionalTests.ClientsControllerTests.Endpoints;
using DblDip.Core.ValueObjects;

namespace DblDip.Api.FunctionalTests
{
    public class ClientsControllerTests : IClassFixture<ApiTestFixture>
    {
        private readonly ApiTestFixture _fixture;
        public ClientsControllerTests(ApiTestFixture fixture)
        {
            _fixture = fixture;
        }


        [Fact]
        public async System.Threading.Tasks.Task Should_CreateClient()
        {
            try
            {
                var context = _fixture.Context;

                StringContent stringContent = new StringContent(JsonConvert.SerializeObject(new { Name = "Quinntyne", Email = "quinntyne@hotmail.com" }), Encoding.UTF8, "application/json");

                using var httpClient = _fixture.CreateAuthenticatedClient();

                var httpResponseMessage = await httpClient.PostAsync(Endpoints.Post.CreateClient, stringContent);

                var response = JsonConvert.DeserializeObject<CreateClient.Response>(await httpResponseMessage.Content.ReadAsStringAsync());

                var sut = context.FindAsync<Client>(response.Client.ClientId);

                Assert.NotEqual(default, response.Client.ClientId);
            } catch (Exception e)
            {
                throw e;
            }
        }

        [Fact]
        public async System.Threading.Tasks.Task Should_RemoveClient()
        {
            var client = ClientBuilder.WithDefaults();

            var context = _fixture.Context;

            var httpClient = _fixture.CreateAuthenticatedClient();

            context.Add(client);

            await context.SaveChangesAsync(default);

            var httpResponseMessage = await httpClient.DeleteAsync(Delete.By(client.ClientId));

            httpResponseMessage.EnsureSuccessStatusCode();

            var removedClient = await context.FindAsync<Client>(client.ClientId);

            Assert.NotEqual(default, removedClient.Deleted);
        }

        [Fact]
        public async System.Threading.Tasks.Task Should_UpdateClient()
        {
            var client = ClientBuilder.WithDefaults();

            var context = _fixture.Context;

            context.Add(client);

            await context.SaveChangesAsync(default);

            StringContent stringContent = new StringContent(JsonConvert.SerializeObject(new { client = client.ToDto() }), Encoding.UTF8, "application/json");

            var httpResponseMessage = await _fixture.CreateAuthenticatedClient().PutAsync(Put.Update, stringContent);

            httpResponseMessage.EnsureSuccessStatusCode();

            var sut = await context.FindAsync<Client>(client.ClientId);

        }

        [Fact]
        public async System.Threading.Tasks.Task Should_GetClients()
        {
            var client = ClientBuilder.WithDefaults();

            var context = _fixture.Context;

            context.Add(client);

            await context.SaveChangesAsync(default);

            var httpResponseMessage = await _fixture.CreateAuthenticatedClient().GetAsync(Get.Clients);

            httpResponseMessage.EnsureSuccessStatusCode();

            var response = JsonConvert.DeserializeObject<GetClients.Response>(await httpResponseMessage.Content.ReadAsStringAsync());

            Assert.True(response.Clients.Any());
        }

        [Fact]
        public async System.Threading.Tasks.Task Should_GetClientById()
        {
            var client = ClientBuilder.WithDefaults();

            var context = _fixture.Context;

            context.Add(client);

            await context.SaveChangesAsync(default);

            var httpResponseMessage = await _fixture.CreateAuthenticatedClient().GetAsync(Get.By(client.ClientId));

            httpResponseMessage.EnsureSuccessStatusCode();

            var response = JsonConvert.DeserializeObject<GetClientById.Response>(await httpResponseMessage.Content.ReadAsStringAsync());

            Assert.NotNull(response);

        }

        internal static class Endpoints
        {
            public static class Post
            {
                public static string CreateClient = "api/clients";
            }

            public static class Put
            {
                public static string Update = "api/clients";
            }

            public static class Delete
            {
                public static string By(Guid clientId)
                {
                    return $"api/clients/{clientId}";
                }
            }

            public static class Get
            {
                public static string Clients = "api/clients";
                public static string By(Guid clientId)
                {
                    return $"api/clients/{clientId}";
                }
            }
        }
    }
}
