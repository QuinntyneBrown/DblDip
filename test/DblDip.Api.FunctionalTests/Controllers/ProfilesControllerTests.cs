using Newtonsoft.Json;
using DblDip.Core.Models;
using DblDip.Domain.Features;
using DblDip.Domain.Features.Profiles;
using DblDip.Testing;
using DblDip.Testing.Builders;
using DblDip.Testing.Builders;
using System;
using System.Linq;
using System.Net.Http;
using System.Text;
using Xunit;
using static DblDip.Api.FunctionalTests.ProfilesControllerTests.Endpoints;

namespace DblDip.Api.FunctionalTests
{
    public class ProfilesControllerTests : IClassFixture<ApiTestFixture>
    {
        private readonly ApiTestFixture _fixture;
        public ProfilesControllerTests(ApiTestFixture fixture)
        {
            _fixture = fixture;
        }


        internal static class Endpoints
        {
            public static class Post
            {
                public static string CreateProfile = "api/profiles";
            }

            public static class Put
            {
                public static string Update = "api/profiles";
            }

            public static class Delete
            {
                public static string By(Guid profileId)
                {
                    return $"api/profiles/{profileId}";
                }
            }

            public static class Get
            {
                public static string Profiles = "api/profiles";
                public static string By(Guid profileId)
                {
                    return $"api/profiles/{profileId}";
                }
            }
        }
    }
}
