using BuildingBlocks.EventStore;
using Microsoft.Extensions.Configuration;
using DblDip.Core.Models;
using DblDip.Core.ValueObjects;
using System;
using System.Linq;
using static DblDip.Core.Constants.Rates;
using DblDip.Core;
using DblDip.Core.Data;

namespace DblDip.Data
{
    public static class DbInitializer
    {
        public static void Initialize(IDblDipDbContext context, IConfiguration configuration)
        {
            RoleConfiguration.Seed(context, configuration);
            SystemLocationConfiguration.Seed(context, configuration);
            CardConfiguration.Seed(context, configuration);
            UserConfiguration.Seed(context, configuration);
            DashboardConfiguration.Seed(context, configuration);

        }

        internal class RoleConfiguration
        {
            public static void Seed(IDblDipDbContext context, IConfiguration configuration)
            {
                AddRoleIfDoesntExists(context, DblDip.Core.Constants.Roles.Lead, nameof(DblDip.Core.Constants.Roles.Lead)).GetAwaiter().GetResult();

                AddRoleIfDoesntExists(context, DblDip.Core.Constants.Roles.Client, nameof(DblDip.Core.Constants.Roles.Client)).GetAwaiter().GetResult();

                AddRoleIfDoesntExists(context, DblDip.Core.Constants.Roles.Photographer, nameof(DblDip.Core.Constants.Roles.Photographer)).GetAwaiter().GetResult();

                AddRoleIfDoesntExists(context, DblDip.Core.Constants.Roles.ProjectManager, nameof(DblDip.Core.Constants.Roles.ProjectManager)).GetAwaiter().GetResult();

                AddRoleIfDoesntExists(context, DblDip.Core.Constants.Roles.SystemAdministrator, nameof(DblDip.Core.Constants.Roles.SystemAdministrator)).GetAwaiter().GetResult();

                async System.Threading.Tasks.Task AddRoleIfDoesntExists(IDblDipDbContext context, Guid roleId, string name)
                {
                    var role = await context.FindAsync<Role>(roleId);

                    role ??= new Role(roleId, name);

                    if (role.DomainEvents.Any())
                    {
                        context.Add(role);
                        await context.SaveChangesAsync(default);
                    }
                }
            }
        }

        internal class RateConfiguration
        {
            public static void Seed(IDblDipDbContext context, IConfiguration configuration)
            {
                SeedRate(nameof(PhotographyRate), Price.Create(100).Value, PhotographyRate);

                SeedRate(nameof(TravelRate), Price.Create(60).Value, TravelRate);

                SeedRate(nameof(ConsulationRate), Price.Create(60).Value, ConsulationRate);

                void SeedRate(string name, Price price, Guid id)
                {
                    var rate = context.FindAsync<Rate>(id).GetAwaiter().GetResult();

                    rate ??= new Rate(name, price, id);

                    if (rate.DomainEvents.Count > 0)
                    {
                        context.Add(rate);

                        context.SaveChangesAsync(default).GetAwaiter().GetResult();
                    }
                }

            }
        }

        internal class CardConfiguration
        {
            public static void Seed(IDblDipDbContext context, IConfiguration configuration)
            {
                var card = context.Set<Card>().FirstOrDefault(x => x.Name == "Leads");

                if (card == null)
                {
                    card = new Card("Leads", "");

                    context.Add(card);

                    context.SaveChangesAsync(default).GetAwaiter().GetResult();
                }
            }
        }

        internal class UserConfiguration
        {
            public static void Seed(IDblDipDbContext context, IConfiguration configuration)
            {
                var username = "quinntynebrown@gmail.com";

                var user = context.Set<User>().SingleOrDefault(x => x.Username == username);

                if (user == null)
                {
                    user = new User(username, "dbldip");

                    user.AddRole(Constants.Roles.SystemAdministrator, nameof(Constants.Roles.SystemAdministrator));

                    context.Add(user);

                    context.SaveChangesAsync(default).GetAwaiter().GetResult();
                }

            }
        }

        internal class DashboardConfiguration
        {
            public static void Seed(IDblDipDbContext context, IConfiguration configuration)
            {
                var user = context.Set<User>().Single(x => x.Username == "quinntynebrown@gmail.com");

                var dashboard = context.Set<Dashboard>().FirstOrDefault(x => x.Name == "Default" && x.ProfileId == user.UserId);

                if (dashboard == null)
                {
                    dashboard = new Dashboard("Default", user.UserId);

                    context.Add(dashboard);

                    context.SaveChangesAsync(default).GetAwaiter().GetResult();
                }
            }
        }

        internal class SystemLocationConfiguration
        {
            public static void Seed(IDblDipDbContext context, IConfiguration configuration)
            {
            }
        }
    }
}
