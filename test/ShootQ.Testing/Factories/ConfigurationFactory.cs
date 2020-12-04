using Microsoft.Extensions.Configuration;

namespace ShootQ.Testing.Factories
{
    public static class ConfigurationFactory
    {
        public static IConfiguration Create()
        {
            var builder = new ConfigurationBuilder();

            return builder.Build();
        }
    }
}
