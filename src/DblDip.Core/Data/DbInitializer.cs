using BuildingBlocks.Abstractions;
using Microsoft.Extensions.Configuration;
using DblDip.Core.Models;
using DblDip.Core.ValueObjects;
using System;
using System.Linq;
using static DblDip.Core.Constants.Rates;
using DblDip.Core;

namespace DblDip.Data
{
    public static class DbInitializer
    {
        public static void Initialize(IAppDbContext context, IConfiguration configuration)
        {
            RoleConfiguration.Seed(context, configuration);
            SystemLocationConfiguration.Seed(context, configuration);
            //CardConfiguration.Seed(context, configuration);
            //UserConfiguration.Seed(context, configuration);
            //DashboardConfiguration.Seed(context, configuration);
        }

        internal class RoleConfiguration
        {
            public static void Seed(IAppDbContext context, IConfiguration configuration)
            {
                AddRoleIfDoesntExists(context, DblDip.Core.Constants.Roles.Client, nameof(DblDip.Core.Constants.Roles.Client)).GetAwaiter().GetResult();
                
                AddRoleIfDoesntExists(context, DblDip.Core.Constants.Roles.Photographer, nameof(DblDip.Core.Constants.Roles.Photographer)).GetAwaiter().GetResult();
                
                AddRoleIfDoesntExists(context, DblDip.Core.Constants.Roles.ProjectManager, nameof(DblDip.Core.Constants.Roles.ProjectManager)).GetAwaiter().GetResult();
                
                AddRoleIfDoesntExists(context, DblDip.Core.Constants.Roles.SystemAdministrator, nameof(DblDip.Core.Constants.Roles.SystemAdministrator)).GetAwaiter().GetResult();

                async System.Threading.Tasks.Task AddRoleIfDoesntExists(IAppDbContext context, Guid roleId, string name)
                {
                    var role = await context.FindAsync<Role>(roleId);

                    role ??= new Role(roleId, name);

                    if (role.DomainEvents.Any())
                    {
                        context.Store(role);
                        await context.SaveChangesAsync(default);
                    }
                }
            }            
        }

        internal class RateConfiguration
        {
            public static void Seed(IAppDbContext context, IConfiguration configuration)
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
                        context.Store(rate);

                        context.SaveChangesAsync(default).GetAwaiter().GetResult();
                    }
                }

            }
        }

        internal class CardConfiguration
        {
            public static void Seed(IAppDbContext context, IConfiguration configuration)
            {
                var card = context.Set<Card>().FirstOrDefault(x => x.Name == "Leads");

                card ??= new Card("Leads","");

                context.Store(card);

                context.SaveChangesAsync(default).GetAwaiter().GetResult();
            }
        }

        internal class UserConfiguration
        {
            public static void Seed(IAppDbContext context, IConfiguration configuration)
            {
                var user = context.Set<User>().FirstOrDefault(x => x.Username == "quinntynebrown@gmail.com");

                user ??= new User("quinntynebrown@gmail.com", "dbldip");

                user.AddRole(Constants.Roles.SystemAdministrator, nameof(Constants.Roles.SystemAdministrator));

                context.Store(user);

                context.SaveChangesAsync(default).GetAwaiter().GetResult();
            }
        }

        internal class DashboardConfiguration
        {
            public static void Seed(IAppDbContext context, IConfiguration configuration)
            {
                var user = context.Set<User>().Single(x => x.Username == "quinntynebrown@gmail.com");

                var dashboard = context.Set<Dashboard>().FirstOrDefault(x => x.Name == "Default" && x.UserId == user.UserId);

                dashboard ??= new Dashboard("Default", user.UserId);

                context.Store(dashboard);

                context.SaveChangesAsync(default).GetAwaiter().GetResult();
            }
        }

        internal class SystemLocationConfiguration
        {
            public static void Seed(IAppDbContext context, IConfiguration configuration)
            {
            }
        }
    }
}
