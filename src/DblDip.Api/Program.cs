using BuildingBlocks.EventStore;
using BuildingBlocks.Core;
using BuildingBlocks.EventStore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using DblDip.Data;
using System;
using System.Linq;
using DblDip.Core.Data;

namespace DblDip.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            ProcessDbCommands(args, host);

            host.Run();
        }

        private static void ProcessDbCommands(string[] args, IHost host)
        {
            var services = (IServiceScopeFactory)host.Services.GetService(typeof(IServiceScopeFactory));

            using (var scope = services.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<DblDipDbContext>();
                var store = scope.ServiceProvider.GetRequiredService<EventStore>();
                var configuration = scope.ServiceProvider.GetRequiredService<IConfiguration>();

                if (args.Contains("ci"))
                    args = new string[4] { "dropdb", "migratedb", "seeddb", "stop" };

                if (args.Contains("dropdb"))
                {
                    context.Database.EnsureDeleted();
                    store.Database.EnsureDeleted();
                }

                if (args.Contains("migratedb"))
                {
                    context.Database.Migrate();
                    store.Database.Migrate();
                }

                if (args.Contains("seeddb"))
                {
                    context.Database.EnsureCreated();
                    store.Database.EnsureCreated();
                    DbInitializer.Initialize(context, store, configuration);
                }

                if (args.Contains("secret"))
                {
                    Console.WriteLine(SecretGenerator.Generate());
                    Environment.Exit(0);
                }

                if (args.Contains("stop"))
                    Environment.Exit(0);
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
