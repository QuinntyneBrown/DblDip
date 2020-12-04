using BuildingBlocks.Core;
using Microsoft.Extensions.Configuration;
using ShootQ.Api;
using System.Collections.Generic;

namespace ShootQ.Testing.Factories
{
    public static class ConfigurationFactory
    {
        private static IConfiguration configuration;
        public static IConfiguration Create()
        {
            if (configuration == null)
            {
                var secret = SecretGenerator.Generate();

                configuration = new ConfigurationBuilder()
                    .AddUserSecrets<Startup>()
                    .AddInMemoryCollection(new Dictionary<string, string>() {
                    { "Seed:DefaultUser:Username" ,"quinntynebrown@gmail.com" },
                    { "Seed:DefaultUser:Password" ,"ShootQ" },
                    { $"{nameof(Authentication)}:{nameof(Authentication.TokenPath)}" ,"/api/users/token" },
                    { $"{nameof(Authentication)}:{nameof(Authentication.ExpirationMinutes)}" ,"10080" },
                    { $"{nameof(Authentication)}:{nameof(Authentication.JwtKey)}", secret },
                    { $"{nameof(Authentication)}:{nameof(Authentication.JwtIssuer)}" ,"localhost" },
                    { $"{nameof(Authentication)}:{nameof(Authentication.JwtAudience)}" ,"all" },
                    { $"{nameof(Authentication)}:{nameof(Authentication.AuthType)}" ,"ShootQ" }
                    })
                    .Build();
            }

            return configuration;
        }
    }
}
