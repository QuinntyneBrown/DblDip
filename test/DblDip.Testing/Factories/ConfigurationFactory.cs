using BuildingBlocks.Core;
using Microsoft.Extensions.Configuration;
using DblDip.Api;
using System.Collections.Generic;
using System.IO;

namespace DblDip.Testing.Factories
{
    public static class ConfigurationFactory
    {
        private static IConfiguration configuration;
        public static IConfiguration Create()
        {
            if (configuration == null)
            {
                var basePath = Path.GetFullPath("../../../../../src/DblDip.Api");

                configuration = new ConfigurationBuilder()
                    .SetBasePath(basePath)
                    .AddJsonFile("appsettings.json",false)
                    .AddUserSecrets<Startup>()
                    .Build();
            }

            return configuration;
        }
    }
}
