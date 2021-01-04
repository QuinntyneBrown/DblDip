using Newtonsoft.Json;
using DblDip.Core.Models;
using DblDip.Domain.Features;
using DblDip.Domain.Features.Libraries;
using DblDip.Testing;
using DblDip.Testing.Builders;
using DblDip.Testing.Builders;
using System;
using System.Linq;
using System.Net.Http;
using System.Text;
using Xunit;
using static DblDip.Api.FunctionalTests.LibrariesControllerTests.Endpoints;

namespace DblDip.Api.FunctionalTests
{
    public class LibrariesControllerTests : IClassFixture<ApiTestFixture>
    {
        private readonly ApiTestFixture _fixture;
        public LibrariesControllerTests(ApiTestFixture fixture)
        {
            _fixture = fixture;
        }


        [Fact]
        public async System.Threading.Tasks.Task Should_CreateLibrary()
        {
            var context = _fixture.Context;

            var library = LibraryDtoBuilder.WithDefaults();

            StringContent stringContent = new StringContent(JsonConvert.SerializeObject(new { library }), Encoding.UTF8, "application/json");

            using var client = _fixture.CreateAuthenticatedClient();

            var httpResponseMessage = await client.PostAsync(Endpoints.Post.CreateLibrary, stringContent);

            var response = JsonConvert.DeserializeObject<CreateLibrary.Response>(await httpResponseMessage.Content.ReadAsStringAsync());

            var sut = context.FindAsync<Library>(response.Library.LibraryId);

            Assert.NotEqual(default, response.Library.LibraryId);
        }

        [Fact]
        public async System.Threading.Tasks.Task Should_RemoveLibrary()
        {
            var library = LibraryBuilder.WithDefaults();

            var context = _fixture.Context;

            var client = _fixture.CreateAuthenticatedClient();

            context.Store(library);

            await context.SaveChangesAsync(default);

            var httpResponseMessage = await client.DeleteAsync(Delete.By(library.LibraryId));

            httpResponseMessage.EnsureSuccessStatusCode();

            var removedLibrary = await context.FindAsync<Library>(library.LibraryId);

            Assert.NotEqual(default, removedLibrary.Deleted);
        }

        [Fact]
        public async System.Threading.Tasks.Task Should_UpdateLibrary()
        {
            var library = LibraryBuilder.WithDefaults();

            var context = _fixture.Context;

            context.Store(library);

            await context.SaveChangesAsync(default);

            StringContent stringContent = new StringContent(JsonConvert.SerializeObject(new { library = library.ToDto() }), Encoding.UTF8, "application/json");

            var httpResponseMessage = await _fixture.CreateAuthenticatedClient().PutAsync(Put.Update, stringContent);

            httpResponseMessage.EnsureSuccessStatusCode();

            var sut = await context.FindAsync<Library>(library.LibraryId);

        }

        [Fact]
        public async System.Threading.Tasks.Task Should_GetLibraries()
        {
            var library = LibraryBuilder.WithDefaults();

            var context = _fixture.Context;

            context.Store(library);

            await context.SaveChangesAsync(default);

            var httpResponseMessage = await _fixture.CreateAuthenticatedClient().GetAsync(Get.Libraries);

            httpResponseMessage.EnsureSuccessStatusCode();

            var response = JsonConvert.DeserializeObject<GetLibraries.Response>(await httpResponseMessage.Content.ReadAsStringAsync());

            Assert.True(response.Libraries.Any());
        }

        [Fact]
        public async System.Threading.Tasks.Task Should_GetLibraryById()
        {
            var library = LibraryBuilder.WithDefaults();

            var context = _fixture.Context;

            context.Store(library);

            await context.SaveChangesAsync(default);

            var httpResponseMessage = await _fixture.CreateAuthenticatedClient().GetAsync(Get.By(library.LibraryId));

            httpResponseMessage.EnsureSuccessStatusCode();

            var response = JsonConvert.DeserializeObject<GetLibraryById.Response>(await httpResponseMessage.Content.ReadAsStringAsync());

            Assert.NotNull(response);

        }

        internal static class Endpoints
        {
            public static class Post
            {
                public static string CreateLibrary = "api/libraries";
            }

            public static class Put
            {
                public static string Update = "api/libraries";
            }

            public static class Delete
            {
                public static string By(Guid libraryId)
                {
                    return $"api/libraries/{libraryId}";
                }
            }

            public static class Get
            {
                public static string Libraries = "api/libraries";
                public static string By(Guid libraryId)
                {
                    return $"api/libraries/{libraryId}";
                }
            }
        }
    }
}
