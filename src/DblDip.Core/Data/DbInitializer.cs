using BuildingBlocks.EventStore;
using DblDip.Core;
using DblDip.Core.Data;
using DblDip.Core.Models;
using DblDip.Core.ValueObjects;
using Microsoft.Extensions.Configuration;
using System;
using System.Linq;
using static DblDip.Core.Constants.Rates;

namespace DblDip.Data
{
    public static class DbInitializer
    {
        public static void Initialize(IDblDipDbContext context, IEventStore store, IConfiguration configuration)
        {
            RoleConfiguration.Seed(store, configuration);
            SystemLocationConfiguration.Seed(store, configuration);
            CardConfiguration.Seed(store, context, configuration);
            UserConfiguration.Seed(context, store, configuration);
            DashboardConfiguration.Seed(context, store, configuration);

        }

        internal class RoleConfiguration
        {
            public static void Seed(IEventStore store, IConfiguration configuration)
            {
                AddRoleIfDoesntExists(store, DblDip.Core.Constants.Roles.Lead, nameof(DblDip.Core.Constants.Roles.Lead)).GetAwaiter().GetResult();

                AddRoleIfDoesntExists(store, DblDip.Core.Constants.Roles.Client, nameof(DblDip.Core.Constants.Roles.Client)).GetAwaiter().GetResult();

                AddRoleIfDoesntExists(store, DblDip.Core.Constants.Roles.Photographer, nameof(DblDip.Core.Constants.Roles.Photographer)).GetAwaiter().GetResult();

                AddRoleIfDoesntExists(store, DblDip.Core.Constants.Roles.ProjectManager, nameof(DblDip.Core.Constants.Roles.ProjectManager)).GetAwaiter().GetResult();

                AddRoleIfDoesntExists(store, DblDip.Core.Constants.Roles.SystemAdministrator, nameof(DblDip.Core.Constants.Roles.SystemAdministrator)).GetAwaiter().GetResult();

                async System.Threading.Tasks.Task AddRoleIfDoesntExists(IEventStore store, Guid roleId, string name)
                {
                    var role = await store.FindAsync<Role>(roleId);

                    role ??= new Role(roleId, name);

                    if (role.DomainEvents.Any())
                    {
                        store.Add(role);
                        await store.SaveChangesAsync(default);
                    }
                }
            }
        }

        internal class RateConfiguration
        {
            public static void Seed(IEventStore store, IConfiguration configuration)
            {
                SeedRate(nameof(PhotographyRate), Price.Create(100).Value, PhotographyRate);

                SeedRate(nameof(TravelRate), Price.Create(60).Value, TravelRate);

                SeedRate(nameof(ConsulationRate), Price.Create(60).Value, ConsulationRate);

                void SeedRate(string name, Price price, Guid id)
                {
                    var rate = store.FindAsync<Rate>(id).GetAwaiter().GetResult();

                    rate ??= new Rate(name, price, id);

                    if (rate.DomainEvents.Count > 0)
                    {
                        store.Add(rate);

                        store.SaveChangesAsync(default).GetAwaiter().GetResult();
                    }
                }

            }
        }

        internal class CardConfiguration
        {
            public static void Seed(IEventStore store, IDblDipDbContext context, IConfiguration configuration)
            {

                var card = context.Set<Card>().FirstOrDefault(x => x.Name == "Leads");

                if (card == null)
                {
                    card = new Card("Leads", "");

                    store.Add(card);

                    store.SaveChangesAsync(default).GetAwaiter().GetResult();
                }

            }
        }

        internal class UserConfiguration
        {
            public static void Seed(IDblDipDbContext context, IEventStore store, IConfiguration configuration)
            {

                var username = (Email)"quinntynebrown@gmail.com";

                var user = context.Users.FirstOrDefault(x => x.Username ==username);

                if (user == null)
                {
                    user = new User(username, "dbldip");

                    user.AddRole(Constants.Roles.SystemAdministrator, nameof(Constants.Roles.SystemAdministrator));

                    store.Add(user);

                    store.SaveChangesAsync(default).GetAwaiter().GetResult();
                }
            }
        }

        internal class DashboardConfiguration
        {
            public static void Seed(IDblDipDbContext context, IEventStore store, IConfiguration configuration)
            {
                var user = context.Set<User>().Single(x => x.Username == "quinntynebrown@gmail.com");

                var dashboard = context.Set<Dashboard>().FirstOrDefault(x => x.Name == "Default" && x.ProfileId == user.UserId);

                if (dashboard == null)
                {
                    dashboard = new Dashboard("Default", user.UserId);

                    store.Add(dashboard);

                    store.SaveChangesAsync(default).GetAwaiter().GetResult();
                }
            }
        }

        internal class SystemLocationConfiguration
        {
            public static void Seed(IEventStore store, IConfiguration configuration)
            {
            }
        }
    }
}
